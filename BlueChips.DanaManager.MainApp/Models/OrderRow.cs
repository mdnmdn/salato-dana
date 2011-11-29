using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueChips.DanaManager.MainApp.Models
{
    public class OrderRow
    {
        // Forn	Item	Ordine	Descrizione	Qty	Dt_CONS	DDT	DDT_DATE	DDT_QTY	Consign	Cons_qty	DDT_Cnsstk_Nr	DDT_Cnststk_Dat	DDT_Cnsstk_QtDoc	Cnsstk_QtyYTD	Action	Qty_YTD


        public string ItemId { get; set; }
        public string OrderId { get; set; }
        public bool Planned { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public DateTime ExpectedDate { get; set; }
        public string DdtCode { get; set; }
        public DateTime? DdtDate { get; set; }
        public int? DdtQty { get; set; }
    }
}
