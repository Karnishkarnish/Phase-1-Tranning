using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Painter
{
    public class PainterWorker
    {
        private readonly ITool _tool;

        public PainterWorker(ITool tool)
        {
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
        }

        public string Paint()
        {
            return _tool.UseTool();
        }
    }
}
