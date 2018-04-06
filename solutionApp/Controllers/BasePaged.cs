using Newtonsoft.Json;
using solutionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.ModelBinding;

namespace solutionApp.Controllers
{
    public static class BasePaged
    {

        /// <summary>
        /// Sorts the elements of a sequence according to a key and the sort order.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="query" />.</typeparam>
        /// <param name="query">A sequence of values to order.</param>
        /// <param name="key">Name of the property of <see cref="TSource"/> by which to sort the elements.</param>
        /// <param name="ascending">True for ascending order, false for descending order.</param>
        /// <returns>An <see cref="T:System.Linq.IOrderedQueryable`1" /> whose elements are sorted according to a key and sort order.</returns>
        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, string key, bool ascending = true)
        { 
            if (string.IsNullOrWhiteSpace(key))
            {
                return query;
            }

            var lambda = (dynamic)CreateExpression(typeof(TSource), key);

            return ascending
                ? Queryable.OrderBy(query, lambda)
                : Queryable.OrderByDescending(query, lambda);
        }

        private static LambdaExpression CreateExpression(Type type, string propertyName)
        {
            var param = Expression.Parameter(type, "x");

            Expression body = param;
            foreach (var member in propertyName.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }

            return Expression.Lambda(body, param);
        }

        public class PagedResults<T>
        {
            /// <summary>
            /// The page number this page represents.
            /// </summary>
            public int pageNumber { get; set; }

            
            public string orderBy { get; set; }

            public bool ascending { get; set; }

            /// <summary>
            /// The size of this page.
            /// </summary>
            public int pageSize { get; set; }

            /// <summary>
            /// The total number of pages available.
            /// </summary>
            public int totalNumberOfPages { get; set; }

            /// <summary>
            /// The total number of records available.
            /// </summary>
            public int totalNumberOfRecords { get; set; }

            /// <summary>
            /// The URL to the next page - if null, there are no more pages.
            /// </summary>
            public string nextPageUrl { get; set; }

            /// <summary>
            /// The records this page represents.
            /// </summary>
            public List<T> results { get; set; }
        }


        public class PagingParameterModel
        {
            const int maxPageSize = 100;

            public int pageNumber { get; set; } = 1;

            public string orderBy { get; set; } = "id";

            public bool ascending { get; set; } = true;

            private int _pageSize { get; set; } = 10;            

            public int pageSize
            {

                get { return _pageSize; }
                set
                {
                    _pageSize = (value > maxPageSize) ? maxPageSize : value;
                }
            }
        }



        public class BasePagedResults<T> 
        {
            public string ToQueryString(object obj)
            {

                return string.Join("&", obj.GetType()
                                           .GetProperties()
                                           .Where(p => p.GetValue(obj, null) != null)
                                           .Select(p => $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.GetValue(obj).ToString())}"));
            }

            /// <summary>
            /// Creates a paged set of results.
            /// </summary>
            /// <typeparam name="T">The type of the source IQueryable.</typeparam>
            /// <typeparam name="TReturn">The type of the returned paged results.</typeparam>
            /// <param name="queryable">The source IQueryable.</param>
            /// <param name="page">The page number you want to retrieve.</param>
            /// <param name="pageSize">The size of the page.</param>
            /// <param name="orderBy">The field or property to order by.</param>
            /// <param name="ascending">Indicates whether or not 
            /// the order should be ascending (true) or descending (false.)</param>
            /// <returns>Returns a paged set of results.</returns>
            public PagedResults<T> CreatePagedResults(
                IQueryable<T> queryable,
                PagingParameterModel pagingParameterModel, ApiController apiController)
            {
                
                //   string Url = "http://localhost:";
                var skipAmount = pagingParameterModel.pageSize * (pagingParameterModel.pageNumber - 1);

                List<T> results = OrderBy<T>(queryable, pagingParameterModel.orderBy, pagingParameterModel.ascending)
                    .Skip(skipAmount)
                    .Take(pagingParameterModel.pageSize).ToList();

                var totalNumberOfRecords = queryable.Count();


                var mod = totalNumberOfRecords % pagingParameterModel.pageSize;
                var totalPageCount = (totalNumberOfRecords / pagingParameterModel.pageSize) + (mod == 0 ? 0 : 1);


                pagingParameterModel.pageNumber = pagingParameterModel.pageNumber + 1;
                

                var nextPageUrl =
                pagingParameterModel.pageNumber-1 == totalPageCount
                    ? null
                    : apiController.Url.Request.RequestUri.AbsolutePath + "?" + ToQueryString(pagingParameterModel);

                return new PagedResults<T>
                {
                    results = results,
                    pageNumber = pagingParameterModel.pageNumber - 1,
                    pageSize = results.Count,
                    totalNumberOfPages = totalPageCount,
                    totalNumberOfRecords = totalNumberOfRecords,
                    nextPageUrl = nextPageUrl,
                    orderBy = pagingParameterModel.orderBy,
                    ascending = pagingParameterModel.ascending
                };
            }

            

        }

    }
}