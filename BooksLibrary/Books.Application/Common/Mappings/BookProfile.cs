using AutoMapper;
using Books.Domain;
using Books.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Common.Mappings
{
    public class BookProfile: Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest=> dest.AuthorName, opt=> opt.MapFrom(src=>
                 $"{src.Author.FirstName} {src.Author.LastName}"));

            CreateMap<BookDto, Book>();
        }
    }
}
