using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_databaser.Models;
using Labb3_databaser.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MongoDB.Bson;
using System.Windows;
using System.Linq;

namespace Labb3_databaser.ViewModels
    {
    public class MovieViewModel : ObservableObject
        {

        #region fields

        private readonly MovieRepository _movieManager;
        private Movie _selectedMovie;
        private ObservableCollection<Movie> _allMovies = new();
        private string _title;
        private string _plot;
        private string _director;
        private string _genre;

        private readonly CommentsRepository _commentManager;
        private ObservableCollection<Comment> _allComments = new();
        private Comment _selectedComment;
        private string _movieComment;
        #endregion




        public MovieViewModel(MovieRepository movieRepository, CommentsRepository commentsRepository)
            {
            _movieManager = movieRepository;
            _commentManager = commentsRepository;
            GetAllMovies();


            AddMovieCommand = new RelayCommand(AddMovie);
            ClearCommand = new RelayCommand(ClearMovieFields);
            DeleteCommand = new RelayCommand(DeleteMovie);
            UpdateCommand = new RelayCommand(UpdateMovie);

            AddCommentCommand = new RelayCommand(AddComment);
            UpdateCommentCommand = new RelayCommand(UpdateComment);
            DeleteCommentCommand = new RelayCommand(DeleteComment);

            }


        #region commands

        public IRelayCommand AddMovieCommand { get; }
        public IRelayCommand ClearCommand { get; }
        public IRelayCommand DeleteCommand { get; }
        public IRelayCommand UpdateCommand { get; }
        public IRelayCommand AddCommentCommand { get; }
        public IRelayCommand UpdateCommentCommand { get; }
        public IRelayCommand DeleteCommentCommand { get; }

        #endregion


        #region PROPERTIES
        public ObservableCollection<Movie> AllMovies
            {
            get { return _allMovies; }
            set
                {
                SetProperty(ref _allMovies, value);
                GetAllMovies();
                }
            }

        public Movie SelectedMovie
            {
            get { return _selectedMovie; }
            set
                {
                if (value is null) return;

                if (SetProperty(_selectedMovie, value, v => _selectedMovie = v))
                    {

                    Title = _selectedMovie.Title;
                    Plot = _selectedMovie.Plot;
                    Director = _selectedMovie.Director;

                    ConvertGenre();
                    GetComments();
                    }

                }
            }

        public Comment SelectedComment
            {
            get { return _selectedComment; }
            set
                {
                if (value is null) return;
                SetProperty(ref _selectedComment, value);
                MovieComment = _selectedComment.MovieComment;
                }
            }

        public string MovieComment
            {
            get { return _movieComment; }
            set { SetProperty(ref _movieComment, value); }
            }

        public ObservableCollection<Comment> AllComments
            {
            get { return _allComments; }

            set
                {
                SetProperty(ref _allComments, value);
                GetComments();
                }
            }
        public string Title
            {
            get { return _title; }

            set
                {
                SetProperty(ref _title, value);
                }
            }

        public string Plot
            {
            get { return _plot; }

            set
                {
                SetProperty(ref _plot, value);
                }
            }

        public string Director
            {
            get { return _director; }

            set
                {
                SetProperty(ref _director, value);
                }
            }

        public string Genre
            {
            get { return _genre; }

            set
                {
                SetProperty(ref _genre, value);
                }
            }

        #endregion

        #region methods
        public void GetAllMovies()
            {

            AllMovies.Clear();
            foreach (var movie in _movieManager.GetAllMovies())
                {
                AllMovies.Add(movie);
                }

            }

        public void GetComments()
            {
            string id = SelectedMovie.Id.ToString();

            AllComments.Clear();
            foreach (var comment in _commentManager.GetComments(id))
                {
                AllComments.Add(comment);
                }

            }

        public void AddMovie()
            {

            var movieObject = new Movie
                {
                Title = Title,
                Plot = Plot,
                Director = Director,
                Genre = new[] { Genre }
                };

            _movieManager.Add(movieObject);
            GetAllMovies();

            }

        public void UpdateMovie()

            {
            var movieObject = new Movie
                {
                Title = Title,
                Plot = Plot,
                Director = Director,
                Genre = new[] { Genre }
                };

            _movieManager.Update(SelectedMovie.Id, movieObject);
            GetAllMovies();
            }

        public void UpdateComment()
            {
            var newComment = new Comment
                {
                MovieComment = MovieComment
                };
            _commentManager.UpdateComment(SelectedMovie.Id, newComment);
            GetComments();
            MovieComment = String.Empty;
            }

        public void DeleteMovie()
            {

            var id = SelectedMovie.Id;
            _movieManager.Delete(id);
            GetAllMovies();

            }

        public void ConvertGenre()
            {
            string separator = ", ";
            string[] genre = _selectedMovie.Genre;

            Genre = string.Join(separator, genre);
            GetAllMovies();
            }

        public void ClearMovieFields()
            {
            Title = String.Empty;
            Plot = String.Empty;
            Director = String.Empty;
            Genre = String.Empty;

            }

        public void AddComment()
            {
        if (SelectedComment is null)
                {
_commentManager.AddComment(new Comment(){MovieId = SelectedMovie.Id.ToString(), MovieComment = MovieComment});
                }
        else
        {
            _commentManager.AddComment(SelectedComment);
        }
        GetComments();
        MovieComment = String.Empty;
            }


        public void DeleteComment()
            {
            var id = SelectedMovie.Id;
            _commentManager.DeleteComment(id);
            GetComments();
            MovieComment = String.Empty;
            }

        #endregion

        }
    }
