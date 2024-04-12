using AnimeStockWebProject.Core.Models.BookTags;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Services.Tests.Comparators
{
    internal class TagViewModelComparator : IComparer
    {
        public int Compare(object? x, object? y)
        {
            TagViewModel bookTag1 = (TagViewModel)x;
            TagViewModel bookTag2 = (TagViewModel)y;

            if (bookTag1 == null || bookTag2 == null)
            {
                return -1;
            }
            if (bookTag1.Id != bookTag2.Id || bookTag1.Name != bookTag2.Name)
            {
                return -1;
            }
            return 0;
        }
    }
}
