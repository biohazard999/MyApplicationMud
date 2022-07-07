using Fluxor;
using Fluxor.Persist.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairWebBlazor.Components.States;

[SkipPersistState]
public record ConnectionState(bool IsOnline);

public class ConnectionStatusFeature : Feature<ConnectionState>
{
    public override string GetName() => nameof(ConnectionState);

    protected override ConnectionState GetInitialState()
        => new ConnectionState(true);
}

[Dispatchable]
public record ConnectionStatusChanged(bool IsOnline);

public static class ConnectionStatusReducers
{
    [ReducerMethod]
    public static ConnectionState ConnectionStatusChanged(ConnectionState state, ConnectionStatusChanged action)
        => state with { IsOnline = action.IsOnline };
}
