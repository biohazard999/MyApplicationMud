using System;
using System.Linq;

using Fluxor;
using Fluxor.Persist.Storage;

namespace MyApplicationMud.Stores;


public record ConnectionStatusChanged(bool IsOnline);

[SkipPersistState]
public record ConnectionState(bool IsOnline);

public class ConnectionStatusFeature : Feature<ConnectionState>
{
    public override string GetName() => nameof(ConnectionState);

    protected override ConnectionState GetInitialState()
        => new ConnectionState(true);
}

public static class ConnectionStatusReducers
{
    [ReducerMethod]
    public static ConnectionState ConnectionStatusChanged(ConnectionState state, ConnectionStatusChanged action)
        => state with { IsOnline = action.IsOnline };
}
