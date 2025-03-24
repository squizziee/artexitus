using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Domain.Entities;
using Artexitus.IdentityMicroservice.Infrastructure.Persistence;
using Artexitus.IdentityMicroservice.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Artexitus.IdentityMicroservice.Tests
{
    public class UserRepositoryTests
    {
        private IUserRepository _userRepository;
        private IdentityDatabaseContext _context;

        public UserRepositoryTests()
        {
            var builder = new DbContextOptionsBuilder<IdentityDatabaseContext>();
            builder.UseSqlite("DataSource=file::memory:?cache=shared");

            _context = new IdentityDatabaseContext(builder.Options);
            _context.Database.EnsureCreated();
            _userRepository = new UserRepository(_context);
        }

        private void FlushContext()
        {
            _context.Database.ExecuteSql($"delete from Users");
            _context.Database.ExecuteSql($"delete from UserProfiles");
            _context.Database.ExecuteSql($"delete from UserRoles");
        }

        [Fact]
        public async void AddUser_ShouldSucceed()
        {
            FlushContext();

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@email.com",
                PasswordHash = "test_hash",
                Profile = new()
                {
                    Id = Guid.NewGuid(),
                    Username = "test_username",
                    Role = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "test_role",
                        Description = "test_description"
                    }
                }
            };

            var oldSize = _context.Users.Count();

            await _userRepository.AddAsync(newUser, CancellationToken.None);
            await _userRepository.SaveChangesAsync(CancellationToken.None);

            var newSize = _context.Users.Count();

            Assert.True(newSize == oldSize + 1);
        }

        [Fact]
        public async Task AddUser_DuplicateEmail_ShouldThrow()
        {
            FlushContext();

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@email.com",
                PasswordHash = "test_hash",
                Profile = new()
                {
                    Id = Guid.NewGuid(),
                    Username = "test_username",
                    Role = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "test_role",
                        Description = "test_description"
                    }
                }
            };

            var duplicateUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@email.com",
                PasswordHash = "test_hash",
                Profile = new()
                {
                    Id = Guid.NewGuid(),
                    Username = "test_username2",
                    Role = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "test_role2",
                        Description = "test_description"
                    }
                }
            };

            var oldSize = _context.Users.Count();

            await _userRepository.AddAsync(newUser, CancellationToken.None);
            await _userRepository.SaveChangesAsync(CancellationToken.None);

            await _userRepository.AddAsync(duplicateUser, CancellationToken.None);
            await Assert.ThrowsAsync<Microsoft.EntityFrameworkCore.DbUpdateException>(
                async () => await _userRepository.SaveChangesAsync(CancellationToken.None)
            );

            var newSize = _context.Users.Count();

            Assert.True(newSize == oldSize + 1);
        }

        [Fact]
        public async void DeleteUser_ShouldSucceed()
        {
            FlushContext();

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@email.com",
                PasswordHash = "test_hash",
                Profile = new()
                {
                    Id = Guid.NewGuid(),
                    Username = "test_username",
                    Role = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "test_role",
                        Description = "test_description"
                    }
                }
            };

            var oldSize = _context.Users.Count();

            await _userRepository.AddAsync(newUser, CancellationToken.None);
            await _userRepository.SaveChangesAsync(CancellationToken.None);

            var newSize = _context.Users.Count();
            Assert.True(newSize == oldSize + 1);

            await _userRepository.DeleteAsync(newUser, CancellationToken.None);
            await _userRepository.SaveChangesAsync(CancellationToken.None);

            var finalSize = _context.Users.Count();
            Assert.True(finalSize == oldSize);
        }

        [Fact]
        public async void DeleteUser_DoesNotExist_ShouldThrow()
        {
            FlushContext();

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@email.com",
                PasswordHash = "test_hash",
                Profile = new()
                {
                    Id = Guid.NewGuid(),
                    Username = "test_username",
                    Role = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "test_role",
                        Description = "test_description"
                    }
                }
            };

            var oldSize = _context.Users.Count();

            await Assert.ThrowsAsync<ResourceDoesNotExistException>(
                async () => await _userRepository.DeleteAsync(newUser, CancellationToken.None)
            );

            var newSize = _context.Users.Count();
            Assert.True(newSize == oldSize);
        }

        [Fact]
        public async void SoftDeleteUser_ShouldSucceed()
        {
            FlushContext();

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@email.com",
                PasswordHash = "test_hash",
                Profile = new()
                {
                    Id = Guid.NewGuid(),
                    Username = "test_username",
                    Role = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "test_role",
                        Description = "test_description"
                    }
                }
            };

            var oldSize = _context.Users.Count();

            await _userRepository.AddAsync(newUser, CancellationToken.None);
            await _userRepository.SaveChangesAsync(CancellationToken.None);

            var newSize = _context.Users.Count();
            Assert.True(newSize == oldSize + 1);

            await _userRepository.SoftDeleteAsync(newUser, CancellationToken.None);
            await _userRepository.SaveChangesAsync(CancellationToken.None);

            var finalSize = _context.Users.Count();
            Assert.True(finalSize == oldSize);
        }

        [Fact]
        public async void SoftDeleteUser_DoesNotExist_ShouldThrow()
        {
            FlushContext();

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@email.com",
                PasswordHash = "test_hash",
                Profile = new()
                {
                    Id = Guid.NewGuid(),
                    Username = "test_username",
                    Role = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "test_role",
                        Description = "test_description"
                    }
                }
            };

            var oldSize = _context.Users.Count();

            await Assert.ThrowsAsync<ResourceDoesNotExistException>(
                async () => await _userRepository.SoftDeleteAsync(newUser, CancellationToken.None)
            );

            var newSize = _context.Users.Count();
            Assert.True(newSize == oldSize);
        }

        [Fact]
        public async Task UpdateUser_ShouldSucceed()
        {
            FlushContext();

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@email.com",
                PasswordHash = "test_hash",
                Profile = new()
                {
                    Id = Guid.NewGuid(),
                    Username = "test_username",
                    Role = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "test_role",
                        Description = "test_description"
                    }
                }
            };

            var updatedUser = new User
            {
                Id = newUser.Id,
                Email = "test2@email.com",
                PasswordHash = "test_hash2",
                Profile = new()
                {
                    Id = Guid.NewGuid(),
                    Username = "test_username2",
                    Role = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "test_role2",
                        Description = "test_description"
                    }
                }
            };

            var oldSize = _context.Users.Count();

            await _userRepository.AddAsync(newUser, CancellationToken.None);
            await _userRepository.SaveChangesAsync(CancellationToken.None);

            await _userRepository.UpdateAsync(updatedUser, CancellationToken.None);
            await _userRepository.SaveChangesAsync(CancellationToken.None);

            var examinedUser = _context.Users.FirstOrDefault(x => x.Id == newUser.Id);

            Assert.NotNull(examinedUser);
            Assert.True(examinedUser.Email == updatedUser.Email);
            Assert.True(examinedUser.PasswordHash == updatedUser.PasswordHash);
            Assert.NotNull(examinedUser.LastUpdatedAt);
        }

        [Fact]
        public async Task UpdateUser_DoesNotExist_ShouldThrow()
        {
            FlushContext();

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@email.com",
                PasswordHash = "test_hash",
                Profile = new()
                {
                    Id = Guid.NewGuid(),
                    Username = "test_username",
                    Role = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "test_role",
                        Description = "test_description"
                    }
                }
            };

            var updatedUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "test2@email.com",
                PasswordHash = "test_hash2",
                Profile = new()
                {
                    Id = Guid.NewGuid(),
                    Username = "test_username2",
                    Role = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "test_role2",
                        Description = "test_description"
                    }
                }
            };

            var oldSize = _context.Users.Count();

            await _userRepository.AddAsync(newUser, CancellationToken.None);
            await _userRepository.SaveChangesAsync(CancellationToken.None);

            await Assert.ThrowsAsync<ResourceDoesNotExistException>(
                async () => await _userRepository.UpdateAsync(updatedUser, CancellationToken.None)
            );

            var examinedUser = _context.Users.FirstOrDefault(x => x.Id == newUser.Id);

            Assert.NotNull(examinedUser);
            Assert.True(examinedUser.Email != updatedUser.Email);
            Assert.True(examinedUser.PasswordHash != updatedUser.PasswordHash);
            Assert.Null(examinedUser.LastUpdatedAt);
        }

        [Fact]
        public async Task GetUsers_ShouldSucceed()
        {
            FlushContext();

            var userCount = 10;

            for (int i = 0; i < userCount; i++)
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Email = $"test{i}@email.com",
                    PasswordHash = $"test_hash{i}",
                    Profile = new()
                    {
                        Id = Guid.NewGuid(),
                        Username = $"test_username{i}",
                        Role = new()
                        {
                            Id = Guid.NewGuid(),
                            Name = $"test_role{i}",
                            Description = $"test_description{i}"
                        }
                    }
                };
                await _userRepository.AddAsync(newUser, CancellationToken.None);
            }

            await _userRepository.SaveChangesAsync(CancellationToken.None);

            var result = await _userRepository.GetAllAsync(CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(userCount, result.Count());
        }

        [Fact]
        public async Task GetUserById_ShouldSucceed()
        {
            FlushContext();

            var userId = Guid.NewGuid();

            var newUser = new User
            {
                Id = userId,
                Email = "test@email.com",
                PasswordHash = "test_hash",
                Profile = new()
                {
                    Id = Guid.NewGuid(),
                    Username = "test_username",
                    Role = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "test_role",
                        Description = "test_description"
                    }
                }
            };

            await _userRepository.AddAsync(newUser, CancellationToken.None);

            await _userRepository.SaveChangesAsync(CancellationToken.None);

            var result = await _userRepository.GetByIdAsync(userId, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(newUser.Email, result.Email);
            Assert.Equal(newUser.PasswordHash, result.PasswordHash);
            Assert.Equal(newUser.ProfileId, result.ProfileId);
        }

        [Fact]
        public async Task GetUserById_DoesNotExist_ShouldReturnNull()
        {
            FlushContext();

            var userId = Guid.NewGuid();

            var newUser = new User
            {
                Id = userId,
                Email = "test@email.com",
                PasswordHash = "test_hash",
                Profile = new()
                {
                    Id = Guid.NewGuid(),
                    Username = "test_username",
                    Role = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "test_role",
                        Description = "test_description"
                    }
                }
            };

            var result = await _userRepository.GetByIdAsync(userId, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetUsersPaginated_ShouldSucceed()
        {
            FlushContext();

            var userCount = 10;

            for (int i = 0; i < userCount; i++)
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Email = $"test{i}@email.com",
                    PasswordHash = $"test_hash{i}",
                    Profile = new()
                    {
                        Id = Guid.NewGuid(),
                        Username = $"test_username{i}",
                        Role = new()
                        {
                            Id = Guid.NewGuid(),
                            Name = $"test_role{i}",
                            Description = $"test_description{i}"
                        }
                    }
                };
                await _userRepository.AddAsync(newUser, CancellationToken.None);
            }

            await _userRepository.SaveChangesAsync(CancellationToken.None);

            var pageNumber = 0;
            var pageSize = 3;

            var result = await _userRepository.GetPaginatedAsync(pageNumber, pageSize, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(pageSize, result.Items.Count());
            Assert.Equal(pageNumber, result.PageNumber);
            Assert.Equal(10 / 3 + 1, result.TotalPages);
        }

        [Fact]
        public async Task GetUsersPaginated_PageNumberTooSmall_ShouldThrow()
        {
            FlushContext();

            var userCount = 10;

            for (int i = 0; i < userCount; i++)
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Email = $"test{i}@email.com",
                    PasswordHash = $"test_hash{i}",
                    Profile = new()
                    {
                        Id = Guid.NewGuid(),
                        Username = $"test_username{i}",
                        Role = new()
                        {
                            Id = Guid.NewGuid(),
                            Name = $"test_role{i}",
                            Description = $"test_description{i}"
                        }
                    }
                };
                await _userRepository.AddAsync(newUser, CancellationToken.None);
            }

            await _userRepository.SaveChangesAsync(CancellationToken.None);

            var pageNumber = -1;
            var pageSize = 3;

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await _userRepository.GetPaginatedAsync(pageNumber, pageSize, CancellationToken.None)
            );
        }

        [Fact]
        public async Task GetUsersPaginated_PageNumberTooBig_ShouldThrow()
        {
            FlushContext();

            var userCount = 10;

            for (int i = 0; i < userCount; i++)
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Email = $"test{i}@email.com",
                    PasswordHash = $"test_hash{i}",
                    Profile = new()
                    {
                        Id = Guid.NewGuid(),
                        Username = $"test_username{i}",
                        Role = new()
                        {
                            Id = Guid.NewGuid(),
                            Name = $"test_role{i}",
                            Description = $"test_description{i}"
                        }
                    }
                };
                await _userRepository.AddAsync(newUser, CancellationToken.None);
            }

            await _userRepository.SaveChangesAsync(CancellationToken.None);

            var pageNumber = 5;
            var pageSize = 3;

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await _userRepository.GetPaginatedAsync(pageNumber, pageSize, CancellationToken.None)
            );
        }

        [Fact]
        public async Task GetUsersPaginated_PageSizeTooSmall_ShouldThrow()
        {
            FlushContext();

            var userCount = 10;

            for (int i = 0; i < userCount; i++)
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Email = $"test{i}@email.com",
                    PasswordHash = $"test_hash{i}",
                    Profile = new()
                    {
                        Id = Guid.NewGuid(),
                        Username = $"test_username{i}",
                        Role = new()
                        {
                            Id = Guid.NewGuid(),
                            Name = $"test_role{i}",
                            Description = $"test_description{i}"
                        }
                    }
                };
                await _userRepository.AddAsync(newUser, CancellationToken.None);
            }

            await _userRepository.SaveChangesAsync(CancellationToken.None);

            var pageNumber = -1;
            var pageSize = 0;

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await _userRepository.GetPaginatedAsync(pageNumber, pageSize, CancellationToken.None)
            );
        }
    }
}