
using BusinessLogicLayer.Contract.IFeatures.ICourse;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Features_Imp.Course_Imp;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Contract.ICommon;
using DataAccessLayer.Repositories.Implementation.Common;
using Moq;
using NUnit.Framework;


namespace BusinessLogicLayer.UnitTests.CourseFeature
{
    [TestFixture]
    public class CourseServiceTests
    {

        private Mock<IUnitOfWork> _mockRepository;
        private ICourseService _courseService;

        [SetUp]
        public void Setup()
        {

            _mockRepository = new Mock<IUnitOfWork>();
            _courseService = new CourseService(_mockRepository.Object, null);
        }

        [Test]
        public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var CourseId = new Guid("12345678-1234-1234-1234-123456789012");
            var expectedCourse = new Course { ID = CourseId, Name = "Test Product" };
            _mockRepository.Setup(repo => repo.GetRepository<Course>().GetByIdAsync(CourseId))
                           .ReturnsAsync(expectedCourse);

            // Act
            var result = await _courseService.GetByIdAsync(CourseId);



            // Assert   
            Assert.That(result, Is.EqualTo(expectedCourse));
        }

        [Test]
        public async Task AddProductAsync_ShouldCallRepositoryAdd_WhenProductIsValid()
        {
            // Arrange
            var newProduct = new Product { Id = 1, Name = "New Product" };

            // Act
            await _productService.AddProductAsync(newProduct);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(newProduct), Times.Once);
        }

        [Test]
        public void AddProductAsync_ShouldThrowArgumentNullException_WhenProductIsNull()
        {
            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _productService.AddProductAsync(null));
        }

        [Test]
        public async Task UpdateProductAsync_ShouldCallRepositoryUpdate_WhenProductIsValid()
        {
            // Arrange
            var existingProduct = new Product { Id = 1, Name = "Updated Product" };

            // Act
            await _productService.UpdateProductAsync(existingProduct);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(existingProduct), Times.Once);
        }

        [Test]
        public async Task DeleteProductAsync_ShouldCallRepositoryDelete_WhenIdIsValid()
        {
            // Arrange
            var productId = 1;

            // Act
            await _productService.DeleteProductAsync(productId);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(productId), Times.Once);
        }


    }
}
