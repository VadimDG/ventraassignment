using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using Moq;
using AutoMapper;
using Intfastructure.Repositories;
using Models;
using Application.Dtos;

namespace Application.Services.Calculate.Tests
{
    public class CalculateServiceTests
    {
        [Fact]
        public async Task Calculate_ReturnsMappedDto_WhenRepositoryReturnsSkus()
        {
            var selected = new[] { 100, 200 };

            var skuRepoMock = new Mock<ISkuRepository>(MockBehavior.Strict);

            var sub = new SubSku
            {
                Id = 11,
                Name = "sub-1",
                Price = 12.34m,
                Ratio = 1,
                HistoryY0s = new HistoryY0 { Units = 2, Amount = 24.68m },
                PlaningY1s = new PlaningY1 { Units = 3, Amount = 37.02m }
            };

            var sku = new Sku
            {
                Id = 1,
                Name = "sku-1",
                SubSkus = [sub]
            };

            var returnedSkus = new List<Sku> { sku };

            skuRepoMock
                .Setup(r => r.GetAllAsync(It.IsAny<int[]>()))
                .ReturnsAsync(returnedSkus);

            var mapperMock = new Mock<IMapper>(MockBehavior.Strict);
            var expectedDto = new Dtos.CalculationDto(); 

            mapperMock
                .Setup(m => m.Map<Dtos.CalculationDto>(It.IsAny<Models.CalculationModel>()))
                .Returns(expectedDto);

            var svc = new CalculateService(skuRepoMock.Object, mapperMock.Object);

            // act
            var result = await svc.Calculate(selected);

            // assert
            Assert.Same(expectedDto, result);

            skuRepoMock.Verify(r => r.GetAllAsync(It.Is<int[]>(ids => ids.SequenceEqual(selected))), Times.Once);
            mapperMock.Verify(m => m.Map<Dtos.CalculationDto>(It.IsAny<Models.CalculationModel>()), Times.Once);
        }

        [Fact]
        public async Task Calculate_CalculatesMultipleSkus_AndInvokesMapper()
        {
            // arrange
            var selected = new[] { 1 };

            var repoMock = new Mock<ISkuRepository>();
            var mapperMock = new Mock<IMapper>();

            var sub1 = new SubSku
            {
                Id = 1,
                Name = "A",
                Price = 10m,
                Ratio = 1,
                HistoryY0s = new HistoryY0 { Units = 5, Amount = 50m },
                PlaningY1s = new PlaningY1 { Units = 6, Amount = 60m }
            };

            var sku1 = new Sku
            {
                Id = 42,
                Name = "Sku42",
                SubSkus = [sub1]
            };

            repoMock.Setup(r => r.GetAllAsync(It.IsAny<int[]>()))
                    .ReturnsAsync(new List<Sku> { sku1 });

            var expected = new CalculationDto();
            mapperMock.Setup(m => m.Map<CalculationDto>(It.IsAny<Models.CalculationModel>()))
                      .Returns(expected);

            var svc = new CalculateService(repoMock.Object, mapperMock.Object);

            // act
            var dto = await svc.Calculate(selected);

            // assert
            Assert.NotNull(dto);
            Assert.Same(expected, dto);

            repoMock.Verify(r => r.GetAllAsync(It.Is<int[]>(ids => ids.SequenceEqual(selected))), Times.Once);
            mapperMock.Verify(m => m.Map<CalculationDto>(It.IsAny<Models.CalculationModel>()), Times.Once);
        }
    }
}