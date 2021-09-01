using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLease.Entities.RequestFeatures {
    public class PagedList<T> : List<T> {
        public MetaData MetaData { get; set; }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize) {
            MetaData = new MetaData {
                TotalCount = count,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }
        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize, int pageOffset) {
            var count = source.Count();
            var items = source
            .Skip(pageOffset)
            .Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}