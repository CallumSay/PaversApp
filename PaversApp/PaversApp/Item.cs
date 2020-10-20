using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaversApp
{
    class Item
    {
        private string sku;

        public void SetSKU(string SKU)
        {
            sku = SKU;
        }
        public string GetSKU(string SKU)
        {
            return sku;
        }
    }
}
