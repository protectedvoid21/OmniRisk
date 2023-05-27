namespace OmniRiskAPI.Dtos; 

public record UserRegisterInfo(string UserName, string Email, string Password);

public record UserLoginInfo(string Email, string Password);

public record AuthToken(string token);