namespace Tests.Services;

[TestFixture]
public class CompanyUseCasesTests : BaseTest
{
    private CompanyService _service;
    private Mock<IMapper> _mapperMock;
    private DbRepository _repository;

    [SetUp]
    public new void Setup()
    {
        base.Setup();
        _mapperMock = new Mock<IMapper>();
        _repository = new DbRepository(Context);
        
        _service = new CompanyService(_repository, _mapperMock.Object, new BaseService(_repository));
    }

    [Test]
    public async Task GetCompanyById_ShouldReturnCompany()
    {
        // Arrange
        var companyId = Guid.NewGuid();
        var company = CreateCompany(companyId, CreateUser(Guid.NewGuid()));

        await Context.Companies.AddAsync(company);
        await Context.SaveChangesAsync();

        _mapperMock.Setup(m => m.Map<CompanyDto>(It.IsAny<Company>())).Returns(new CompanyDto()
        {
            Id = companyId,
            Name = "John Doe",
            Phone = "12343",
            Email = "johndoe@gmail.com"
        });

        // Act
        var result = await _service.GetByIdAsync(companyId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(companyId);
    }

    [Test]
    public async Task PostCompany_ShouldAddCompany()
    {
        // Arrange
        var companyId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var user = CreateUser(userId);

        await Context.Users.AddAsync(user);
        await Context.SaveChangesAsync();

        var companyDto = new CompanyDto()
        {
            Id = companyId,
            Name = "John Doe",
            Phone = "12343",
            Email = "johndoe@gmail.com",
            UserId = userId
        };

        var company = CreateCompany(companyId, user);

        _mapperMock.Setup(m => m.Map<Company>(It.IsAny<CompanyDto>())).Returns(company);
        _mapperMock.Setup(m => m.Map<CompanyDto>(It.IsAny<Company>())).Returns(companyDto);

        // Act
        await _service.PostAsync(companyDto);

        // Assert
        var addedCompany = await Context.Companies.FindAsync(companyId);
        addedCompany.Should().NotBeNull();
        addedCompany.Name.Should().Be(companyDto.Name);
    }

    [Test]
    public async Task GetAllCompanies_ShouldReturnAllCompanies()
    {
        // Arrange
        var user = CreateUser(Guid.NewGuid());
        var companies = new List<Company>
        {
            CreateCompany(Guid.NewGuid(), user),
            CreateCompany(Guid.NewGuid(), user)
        };

        await Context.Companies.AddRangeAsync(companies);
        await Context.SaveChangesAsync();

        _mapperMock.Setup(m => m.Map<IEnumerable<CompanyDto>>(It.IsAny<List<CompanyDto>>()))
            .Returns(companies.Select(b => new CompanyDto() { Id = b.Id, Name = b.Name }).ToList());

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
    }

    [Test]
    public async Task DeleteCompany_ShouldRemoveCompany()
    {
        // Arrange
        var companyId = Guid.NewGuid();
        var author = CreateCompany(companyId, CreateUser(Guid.NewGuid()));

        await Context.Companies.AddAsync(author);
        await Context.SaveChangesAsync();

        // Act
        await _service.DeleteByIdAsync(companyId);
        var deletedAuthor = await Context.Companies.FindAsync(companyId);

        // Assert
        deletedAuthor.Should().BeNull();
    }
    
    [Test]
    public async Task PutBook_ShouldUpdateBook()
    {
        // Arrange
        var companyId = Guid.NewGuid();
        var user = CreateUser(Guid.NewGuid());
        var company = CreateCompany(companyId, user);

        await Context.Companies.AddAsync(company);
        await Context.SaveChangesAsync();

        var updatedDto = new CompanyDto()
        {
            Id = companyId,
            Name = "Updated Company",
            Phone = "12345",
            Email = "updated@gmail.com",
        };

        _mapperMock.Setup(m => m.Map(It.IsAny<CompanyDto>(), It.IsAny<Company>())).Callback((CompanyDto dto, Company entity) =>
        {
            entity.Name = dto.Name;
            entity.Phone = dto.Phone;
            entity.Email = dto.Email;
            entity.DateUpdated = DateTime.UtcNow;
        });

        // Act
        var result = await _service.PutAsync(updatedDto);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Updated Company");
    }

    private Company CreateCompany(Guid companyId, User user)
    {
        return new Company
        {
            Id = companyId,
            Name = "John Doe",
            Phone = "12343",
            Email = "johndoe@gmail.com",
            User = user
        };
    }
    
    private User CreateUser(Guid userId)
    {
        return new User()
        {
            Id = userId,
            Login = "Login",
            Password = "123",
            Salt = "342432",
            Role = "user",
            IsBlocked = false
        };
    }

}