namespace MyApplicationMud.Stores;


public record FetchDataAction();

public record FetchDataResultAction(IEnumerable<WeatherForecast> Forecasts);

[FeatureState]
public record WeatherState
{
    public bool IsLoading { get; init; }
    public IEnumerable<WeatherForecast> Forecasts { get; init; } = Array.Empty<WeatherForecast>();
}

public static class Reducers
{
    [ReducerMethod(typeof(FetchDataAction))]
    public static WeatherState ReduceFetchDataAction(WeatherState state)
        => state with
        {
            IsLoading = true
        };

    [ReducerMethod]
    public static WeatherState ReduceFetchDataResultAction(WeatherState state, FetchDataResultAction action)
        => state with
        {
            IsLoading = false,
            Forecasts = action.Forecasts
        };
}

public class FetchDataActionEffect : Effect<FetchDataAction>
{
    private readonly IHttpClientFactory HttpClientFactory;

    public FetchDataActionEffect(IHttpClientFactory httpClientFactory)
    {
        HttpClientFactory = httpClientFactory;
    }

    public override async Task HandleAsync(FetchDataAction action, IDispatcher dispatcher)
    {
        try
        {
            var httpClient = HttpClientFactory.CreateClient("backend");
            var forecasts = await httpClient.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
            if (forecasts is not null)
            {
                dispatcher.Dispatch(new FetchDataResultAction(forecasts));
            }
        }
        catch (Exception ex)
        {

        }
    }
}
