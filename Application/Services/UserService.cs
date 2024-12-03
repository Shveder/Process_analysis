namespace Application.Services;

[AutoInterface]
public class UserService(IDbRepository repository) : IUserService
{
    public async Task ChangePassword(ChangePasswordRequest request)
    {
        var user = await repository.Get<User>(model => model.Id == request.Id).FirstOrDefaultAsync();

        string prevPassword = request.PreviousPassword; 
        prevPassword = Hash(prevPassword);
        prevPassword = Hash(prevPassword + user?.Salt);
        
        if (!(prevPassword == user.Password))
            throw new IncorrectDataException("Password is incorrect!");
        
        if (prevPassword == request.NewPassword)
            throw new IncorrectDataException("New password must be indifferent!");
        
        if (request.NewPassword.Length is < 4 or > 32)
            throw new IncorrectDataException("Password must be between 4 and 32 characters long!");
        
        request.NewPassword = Hash(request.NewPassword);
        request.NewPassword = Hash(request.NewPassword + user?.Salt);
        
        user.Password = request.NewPassword;
        user.DateUpdated = DateTime.UtcNow;
        
        await repository.Update(user);
        await repository.SaveChangesAsync();

        await AddRecentPassword(user, request.PreviousPassword);
    }
    
    private async Task AddRecentPassword(User user, string oldPassword)
    {
        var recentPassword = new RecentPassword
        {
            Id = Guid.NewGuid(),
            Password = oldPassword,
            User = user
        };
        await repository.Add(recentPassword);
        await repository.SaveChangesAsync();
    }
    
    private string Hash(string inputString)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("x2"));
            
            return sb.ToString();
        }
    }
}