namespace RunMate.Domain.Entities;

/// <summary>
/// Represents a user in the RunMate app.
/// </summary>
public class User
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// The last name of the user.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// The email address of the user.
    /// </summary>
    public string Email { get; private set; }

    public User(string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public User(Guid id, string firstName, string lastName, string email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public void UpdateProfile(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}