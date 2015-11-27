﻿using BookInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookInventory.Helpers
{
    public static class HelperClass
    {
        public static List<Author> converStringToListAuthors(string text)
        {
            List<Author> result = new List<Author>();
            char delimiter = ',';
            string[] splittedText = Array.ConvertAll(text.Split(delimiter), p => p.Trim());
            foreach (var item in splittedText)
            {
                result.Add(new Author { AuthorName = item });
            }
            return result;
        }

        public static List<Category> converStringToListCategories(string text)
        {
            List<Category> result = new List<Category>();
            char delimiter = ',';
            string[] splittedText = Array.ConvertAll(text.Split(delimiter), p => p.Trim());
            foreach (var item in splittedText)
            {
                result.Add(new Category { CategoryName = item});
            }
            return result;
        }
    }
}