using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Mappings;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using EnglishVocab.Application.Features.VocabularyManagement.Words.Queries;
using EnglishVocab.Domain.Entities;
using Moq;
using Xunit;

namespace EnglishVocab.Tests.UnitTests.Features.Words
{
    public class GetAllWordsQueryHandlerTests
    {
        private readonly Mock<IWordRepository> _mockWordRepository;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IDifficultyLevelRepository> _mockDifficultyLevelRepository;
        private readonly IMapper _mapper;
        
        public GetAllWordsQueryHandlerTests()
        {
            _mockWordRepository = new Mock<IWordRepository>();
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockDifficultyLevelRepository = new Mock<IDifficultyLevelRepository>();
            
            // Configure AutoMapper with our mapping profile
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WordMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }
        
        [Fact]
        public async Task Handle_WithoutFilters_ShouldReturnAllWords()
        {
            // Arrange
            var words = new List<Word>
            {
                new Word { 
                    Id = Guid.NewGuid(), 
                    English = "Hello", 
                    Vietnamese = "Xin chào",
                    DateCreated = DateTime.Now,
                    CreatedBy = "Test"
                },
                new Word { 
                    Id = Guid.NewGuid(), 
                    English = "Goodbye", 
                    Vietnamese = "Tạm biệt",
                    DateCreated = DateTime.Now,
                    CreatedBy = "Test"
                }
            };
            
            _mockWordRepository
                .Setup(repo => repo.GetAllAsync(
                    It.IsAny<string>(), 
                    It.IsAny<Guid?>(), 
                    It.IsAny<Guid?>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(words);
                
            var handler = new GetAllWordsQueryHandler(
                _mockWordRepository.Object,
                _mockCategoryRepository.Object,
                _mockDifficultyLevelRepository.Object,
                _mapper);
                
            var query = new GetAllWordsQuery();
            
            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, w => w.English == "Hello");
            Assert.Contains(result, w => w.English == "Goodbye");
        }
        
        [Fact]
        public async Task Handle_WithSearchTerm_ShouldFilterWords()
        {
            // Arrange
            var searchTerm = "hello";
            var words = new List<Word>
            {
                new Word { 
                    Id = Guid.NewGuid(), 
                    English = "Hello", 
                    Vietnamese = "Xin chào",
                    DateCreated = DateTime.Now,
                    CreatedBy = "Test"
                }
            };
            
            _mockWordRepository
                .Setup(repo => repo.GetAllAsync(
                    searchTerm, 
                    It.IsAny<Guid?>(), 
                    It.IsAny<Guid?>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(words);
                
            var handler = new GetAllWordsQueryHandler(
                _mockWordRepository.Object,
                _mockCategoryRepository.Object,
                _mockDifficultyLevelRepository.Object,
                _mapper);
                
            var query = new GetAllWordsQuery { SearchTerm = searchTerm };
            
            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            
            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Hello", result.First().English);
        }
        
        [Fact]
        public async Task Handle_WithCategoryId_ShouldIncludeCategoryName()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var categoryName = "Greetings";
            
            var words = new List<Word>
            {
                new Word { 
                    Id = Guid.NewGuid(), 
                    English = "Hello", 
                    Vietnamese = "Xin chào",
                    CategoryId = categoryId,
                    DateCreated = DateTime.Now,
                    CreatedBy = "Test"
                }
            };
            
            var category = new Category
            {
                Id = categoryId,
                Name = categoryName
            };
            
            _mockWordRepository
                .Setup(repo => repo.GetAllAsync(
                    It.IsAny<string>(), 
                    categoryId, 
                    It.IsAny<Guid?>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(words);
                
            _mockCategoryRepository
                .Setup(repo => repo.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(category);
                
            var handler = new GetAllWordsQueryHandler(
                _mockWordRepository.Object,
                _mockCategoryRepository.Object,
                _mockDifficultyLevelRepository.Object,
                _mapper);
                
            var query = new GetAllWordsQuery { CategoryId = categoryId };
            
            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            
            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(categoryName, result.First().CategoryName);
        }
    }
} 