﻿using DatabaseP.models;
using Microsoft.AspNetCore.Mvc;
using MoveisAPI.Helpers;

namespace MoveisAPI.DTOs
{
    public class MovieCreationDTO
    {
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Trailer { get; set; }
        public bool InTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IFormFile? Poster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int>? GenresIds { get; set; }


        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int>? movieTheatersIds { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<MoviesActorsCreationDTO>>))]
        public List<MoviesActorsCreationDTO>? Actors { get; set; }
    }
}
