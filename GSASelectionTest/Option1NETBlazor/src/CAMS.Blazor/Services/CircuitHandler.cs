using Microsoft.AspNetCore.Components.Server.Circuits;

namespace CAMS.Blazor.Services
{
    public class CamsCircuitHandler : CircuitHandler
    {
        private readonly ILogger<CamsCircuitHandler> _logger;

        public CamsCircuitHandler(ILogger<CamsCircuitHandler> logger)
        {
            _logger = logger;
        }

        public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Circuit opened: {CircuitId}", circuit.Id);
            return base.OnCircuitOpenedAsync(circuit, cancellationToken);
        }

        public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Circuit closed: {CircuitId}", circuit.Id);
            return base.OnCircuitClosedAsync(circuit, cancellationToken);
        }

        public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Connection down: {CircuitId}", circuit.Id);
            return base.OnConnectionDownAsync(circuit, cancellationToken);
        }

        public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Connection up: {CircuitId}", circuit.Id);
            return base.OnConnectionUpAsync(circuit, cancellationToken);
        }
    }
}
