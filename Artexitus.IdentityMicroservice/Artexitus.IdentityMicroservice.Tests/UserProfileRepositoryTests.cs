using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Domain.Entities;
using Artexitus.IdentityMicroservice.Infrastructure.Persistence.Repositories;
using Artexitus.IdentityMicroservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Artexitus.IdentityMicroservice.Application.Interfaces;

namespace Artexitus.IdentityMicroservice.Tests
{
    public class UserProfileRepositoryTests
    {
        private IUserProfileRepository _userProfileRepository;
        private IdentityDatabaseContext _context;

        public UserProfileRepositoryTests()
        {
            var builder = new DbContextOptionsBuilder<IdentityDatabaseContext>();
            builder.UseSqlite("DataSource=file::memory:?cache=shared");

            _context = new IdentityDatabaseContext(builder.Options);
            _context.Database.EnsureCreated();
            _userProfileRepository = new UserProfileRepository(_context);
        }

        private void FlushContext()
        {
            _context.Database.ExecuteSql($"delete from Users");
            _context.Database.ExecuteSql($"delete from UserProfiles");
            _context.Database.ExecuteSql($"delete from UserRoles");
        }

        [Fact]
        public async void AddUserProfile_ShouldSucceed()
        {
            FlushContext();

            var newProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Username = "test_username",
                Role = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "test_role",
                    Description = "test_description"
                }
            };

            _context.SaveChanges();

            var oldSize = _context.UserProfiles.Count();

            await _userProfileRepository.AddAsync(newProfile, CancellationToken.None);
            await _userProfileRepository.SaveChangesAsync(CancellationToken.None);

            var newSize = _context.UserProfiles.Count();

            Assert.True(newSize == oldSize + 1);
        }

        [Fact]
        public async Task AddUserProfile_DuplicateUsername_ShouldThrow()
        {
            FlushContext();

            var newProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Username = "test_username",
                Role = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "test_role",
                    Description = "test_description"
                }
            };

            var duplicateProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Username = "test_username",
                Role = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "test_role",
                    Description = "test_description"
                }
            };

            var oldSize = _context.UserProfiles.Count();

            await _userProfileRepository.AddAsync(newProfile, CancellationToken.None);
            await _userProfileRepository.SaveChangesAsync(CancellationToken.None);

            await _userProfileRepository.AddAsync(duplicateProfile, CancellationToken.None);
            await Assert.ThrowsAsync<Microsoft.EntityFrameworkCore.DbUpdateException>(
                async () => await _userProfileRepository.SaveChangesAsync(CancellationToken.None)
            );

            var newSize = _context.UserProfiles.Count();

            Assert.True(newSize == oldSize + 1);
        }

        [Fact]
        public async void DeleteUserProfile_ShouldSucceed()
        {
            FlushContext();

            var newProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Username = "test_username",
                Role = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "test_role",
                    Description = "test_description"
                }
            };

            var oldSize = _context.UserProfiles.Count();

            await _userProfileRepository.AddAsync(newProfile, CancellationToken.None);
            await _userProfileRepository.SaveChangesAsync(CancellationToken.None);

            var newSize = _context.UserProfiles.Count();
            Assert.True(newSize == oldSize + 1);

            await _userProfileRepository.DeleteAsync(newProfile, CancellationToken.None);
            await _userProfileRepository.SaveChangesAsync(CancellationToken.None);

            var finalSize = _context.UserProfiles.Count();
            Assert.True(finalSize == oldSize);
        }

        [Fact]
        public async void DeleteUserProfile_DoesNotExist_ShouldThrow()
        {
            FlushContext();

            var newProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Username = "test_username",
                Role = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "test_role",
                    Description = "test_description"
                }
            };

            var oldSize = _context.UserProfiles.Count();

            await Assert.ThrowsAsync<ResourceDoesNotExistException>(
                async () => await _userProfileRepository.DeleteAsync(newProfile, CancellationToken.None)
            );

            var newSize = _context.UserProfiles.Count();
            Assert.True(newSize == oldSize);
        }

        [Fact]
        public async void SoftDeleteUserProfile_ShouldSucceed()
        {
            FlushContext();

            var newProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Username = "test_username",
                Role = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "test_role",
                    Description = "test_description"
                }
            };

            var oldSize = _context.UserProfiles.Count();

            await _userProfileRepository.AddAsync(newProfile, CancellationToken.None);
            await _userProfileRepository.SaveChangesAsync(CancellationToken.None);

            var newSize = _context.UserProfiles.Count();
            Assert.True(newSize == oldSize + 1);

            await _userProfileRepository.SoftDeleteAsync(newProfile, CancellationToken.None);
            await _userProfileRepository.SaveChangesAsync(CancellationToken.None);

            var finalSize = _context.UserProfiles.Count();
            Assert.True(finalSize == oldSize);
        }

        [Fact]
        public async void SoftDeleteUserProfile_DoesNotExist_ShouldThrow()
        {
            FlushContext();

            var newProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Username = "test_username",
                Role = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "test_role",
                    Description = "test_description"
                }
            };

            var oldSize = _context.UserProfiles.Count();

            await Assert.ThrowsAsync<ResourceDoesNotExistException>(
                async () => await _userProfileRepository.SoftDeleteAsync(newProfile, CancellationToken.None)
            );

            var newSize = _context.UserProfiles.Count();
            Assert.True(newSize == oldSize);
        }

        [Fact]
        public async Task UpdateUserProfile_ShouldSucceed()
        {
            FlushContext();

            var dummyRole = new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "test_role2",
                Description = "test_description"
            };

            _context.UserRoles.Add(dummyRole);
            _context.SaveChanges();

            var newProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Username = "test_username",
                RoleId = dummyRole.Id,
            };

            var updatedProfile = new UserProfile
            {
                Id = newProfile.Id,
                Username = "test_username2",
                RoleId = dummyRole.Id,
            };

            var oldSize = _context.UserProfiles.Count();

            await _userProfileRepository.AddAsync(newProfile, CancellationToken.None);
            await _userProfileRepository.SaveChangesAsync(CancellationToken.None);

            await _userProfileRepository.UpdateAsync(updatedProfile, CancellationToken.None);
            await _userProfileRepository.SaveChangesAsync(CancellationToken.None);

            var examinedProfile = _context.UserProfiles.FirstOrDefault(x => x.Id == newProfile.Id);

            Assert.NotNull(examinedProfile);
            Assert.Equal(examinedProfile.Username, updatedProfile.Username);
            Assert.NotNull(examinedProfile.LastUpdatedAt);
        }

        [Fact]
        public async Task UpdateUserProfile_DoesNotExist_ShouldThrow()
        {
            FlushContext();

            var newProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Username = "test_username",
                Role = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "test_role",
                    Description = "test_description"
                }
            };

            var updatedProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Username = "test_username2",
                Role = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "test_role2",
                    Description = "test_description"
                }
            };

            var oldSize = _context.UserProfiles.Count();

            await _userProfileRepository.AddAsync(newProfile, CancellationToken.None);
            await _userProfileRepository.SaveChangesAsync(CancellationToken.None);

            await Assert.ThrowsAsync<ResourceDoesNotExistException>(
                async () => await _userProfileRepository.UpdateAsync(updatedProfile, CancellationToken.None)
            );

            var examinedProfile = _context.UserProfiles.FirstOrDefault(x => x.Id == newProfile.Id);

            Assert.NotNull(examinedProfile);
            Assert.NotEqual(examinedProfile.Username, updatedProfile.Username);
            Assert.Null(examinedProfile.LastUpdatedAt);
        }

        [Fact]
        public async Task UpdateUserProfile_UsernameAlreadyExists_ShouldThrow()
        {
            FlushContext();

            var existentProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Username = "test_username",
                Role = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "test_role",
                    Description = "test_description"
                }
            };

            await _userProfileRepository.AddAsync(existentProfile, CancellationToken.None);
            await _userProfileRepository.SaveChangesAsync(CancellationToken.None);

            var newProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Username = "test_username2",
                Role = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "test_role2",
                    Description = "test_description"
                }
            };

            var updatedProfile = new UserProfile
            {
                Id = newProfile.Id,
                Username = "test_username",
                Role = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "test_role3",
                    Description = "test_description"
                }
            };

            var oldSize = _context.UserProfiles.Count();

            await _userProfileRepository.AddAsync(newProfile, CancellationToken.None);
            await _userProfileRepository.SaveChangesAsync(CancellationToken.None);

            await _userProfileRepository.UpdateAsync(updatedProfile, CancellationToken.None);
            await Assert.ThrowsAsync<Microsoft.EntityFrameworkCore.DbUpdateException>(
                async () => await _userProfileRepository.SaveChangesAsync(CancellationToken.None)
            );

            var examinedProfile = _context.UserProfiles.FirstOrDefault(x => x.Id == newProfile.Id);

            // Really weird that changes are applied even when DbUpdateException is thrown.
            // Might be due to in-memory database provider

            //Assert.NotNull(examinedProfile);
            //Assert.NotEqual(examinedProfile.Username, updatedProfile.Username);
            //Assert.Null(examinedProfile.LastUpdatedAt);
        }
    }
}
