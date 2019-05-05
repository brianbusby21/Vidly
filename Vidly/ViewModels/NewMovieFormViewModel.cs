using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class NewMovieFormViewModel //Pure View Model
    {
        public IEnumerable<Genre> MovieGenres { get; set; }

        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }


        [Display(Name = "Genre")]
        [Required]
        public byte? MovieGenreId { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }


        [Display(Name = "Number in Stock")]
        [Required]
        [Range(1, 20)]
        public int? NumberInStock { get; set; }


        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }

        public NewMovieFormViewModel()
        {
            Id = 0;
        }

        public NewMovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            MovieGenreId = movie.MovieGenreId;
        }
    }
}