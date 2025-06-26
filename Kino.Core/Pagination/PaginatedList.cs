namespace Kino.Core.Pagination
{
    /// <summary>
    /// Predstavlja listu sa podrškom za paginaciju (straničenje).
    /// Nasleđuje List<T> i omogućava kreiranje podliste na osnovu stranice i veličine stranice.
    /// </summary>
    public class PaginatedList<T> : List<T>
    {
        /// <summary>
        /// Inicijalizuje novu instancu klase PaginatedList sa zadatim stavkama.
        /// </summary>
        /// <param name="items">Stavke koje će biti deo paginirane liste.</param>
        public PaginatedList(List<T> items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// Kreira novu paginiranu listu na osnovu izvora podataka, broja stranice i veličine stranice.
        /// </summary>
        /// <param name="source">Izvor podataka (IQueryable).</param>
        /// <param name="pageIndex">Broj stranice (počinje od 1).</param>
        /// <param name="pageSize">Veličina stranice (broj stavki po stranici).</param>
        /// <returns>Paginirana lista sa stavkama za traženu stranicu.</returns>
        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items);
        }
    }
}

// Ovo je prijedlog proširenja klase
//namespace Kino.Core.Pagination
//{
//    /// <summary>
//    /// Generička lista sa podrškom za paginaciju, uključujući informacije o ukupnom broju stavki i stranica.
//    /// </summary>
//    public class PaginatedList<T> : List<T>
//    {
//        /// <summary>
//        /// Trenutni broj stranice (počinje od 1).
//        /// </summary>
//        public int PageIndex { get; }

//        /// <summary>
//        /// Ukupan broj stranica.
//        /// </summary>
//        public int TotalPages { get; }

//        /// <summary>
//        /// Broj stavki po stranici.
//        /// </summary>
//        public int PageSize { get; }

//        /// <summary>
//        /// Ukupan broj stavki u izvoru podataka.
//        /// </summary>
//        public int TotalCount { get; }

//        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
//        {
//            TotalCount = count;
//            PageSize = pageSize;
//            PageIndex = pageIndex;
//            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

//            this.AddRange(items);
//        }

//        /// <summary>
//        /// Kreira novu paginiranu listu na osnovu izvora podataka, broja stranice i veličine stranice.
//        /// </summary>
//        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
//        {
//            var count = source.Count();
//            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
//            return new PaginatedList<T>(items, count, pageIndex, pageSize);
//        }
//    }
//}
