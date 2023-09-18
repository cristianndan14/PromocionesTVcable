using System.Collections.Generic;

namespace Dato
{
    public class ProcedimientoAlmacenadoOracle
    {
        private string comandText;
        private List<ParametroOracle> parameters = new List<ParametroOracle>();

        public string ComandText { get => comandText; set => comandText = value; }
        public List<ParametroOracle> Parameters { get => parameters; set => parameters = value; }
    }
}
