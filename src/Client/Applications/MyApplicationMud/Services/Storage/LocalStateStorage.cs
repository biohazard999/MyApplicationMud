using Blazored.LocalStorage;

using Fluxor.Persist.Storage;

namespace MyApplicationMud.Services.Storage;

public record LocalStateStorage(ILocalStorageService LocalStorage) : IStringStateStorage
{
    public async ValueTask<string> GetStateJsonAsync(string statename)
        => await LocalStorage.GetItemAsStringAsync(statename);

    public async ValueTask StoreStateJsonAsync(string statename, string json)
        => await LocalStorage.SetItemAsStringAsync(statename, json);
}
