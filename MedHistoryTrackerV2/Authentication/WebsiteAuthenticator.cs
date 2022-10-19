namespace MedHistoryTrackerV2.Authentication;

public class WebsiteAuthenticator : AuthenticationStateProvider
{
    private readonly ProtectedLocalStorage _protectedLocalStorage;

    private List<User> _users = new List<User> {
                                new User { Id = 1, Name = "Hitesh Ram Kotha", UserName = "hiteshramk", Email = "hiteshram.kotha@gmail.com", Password = "Hitesh@1" },
                                new User { Id = 2, Name = "Meghanath Kotha", UserName = "meghanathk", Email = "kmnath.vja@gmail.com", Password = "Meghanath@1" }

    };
    public WebsiteAuthenticator(ProtectedLocalStorage protectedLocalStorage)
    {
        _protectedLocalStorage = protectedLocalStorage;
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = new ClaimsPrincipal();

        try
        {
            var storedPrincipal = await _protectedLocalStorage.GetAsync<string>("identity");
            if (storedPrincipal.Success)
            {
                var user = JsonConvert.DeserializeObject<User>(storedPrincipal.Value);
                var (_, isLookUpSuccess) = LookUpUser(user.UserName, user.Password);

                if (isLookUpSuccess)
                {
                    var identity = CreateIdentityFromUser(user);
                    principal = new(identity);
                }
            }
        }
        catch
        {

        }

        return new AuthenticationState(principal);
    }

    public async Task<bool> LoginAsync(LoginForm loginForm)
    {
        var (userInDatabase, isSuccess) = LookUpUser(loginForm.UserName, loginForm.Password);
        var principal = new ClaimsPrincipal();

        if (isSuccess)
        {
            var identity = CreateIdentityFromUser(userInDatabase);
            principal = new ClaimsPrincipal(identity);
            await _protectedLocalStorage.SetAsync("identity", JsonConvert.SerializeObject(userInDatabase));
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        return isSuccess;
    }

    public async Task LogoutAsync()
    {
        await _protectedLocalStorage.DeleteAsync("identity");
        var principal = new ClaimsPrincipal();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    private (User, bool) LookUpUser(string username, string password)
    {
        var result = _users.Where(u => u.UserName.Equals(username) && u.Password.Equals(password)).FirstOrDefault();
        return (result, result is not null);
    }
    private static ClaimsIdentity CreateIdentityFromUser(User user)
    {
        return new ClaimsIdentity(new Claim[]
        {
            new (ClaimTypes.Sid, user.Id.ToString()),
            new (ClaimTypes.Name, user.Name),
            new (ClaimTypes.Hash, user.Password),
            new (ClaimTypes.NameIdentifier,user.UserName)
        }, "MedHistoryTracker");
    }
}

