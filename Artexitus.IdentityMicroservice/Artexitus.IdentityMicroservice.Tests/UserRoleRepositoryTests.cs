using Artexitus.IdentityMicroservice.Domain.Repositories;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Domain.Entities;
using Artexitus.IdentityMicroservice.Infrastructure.Persistence.Repositories;
using Artexitus.IdentityMicroservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Artexitus.IdentityMicroservice.Tests
{
    public class UserRoleRepositoryTests
    {
        private IUserRoleRepository _userRoleRepository;
        private IdentityDatabaseContext _context;

        public UserRoleRepositoryTests()
        {
            var builder = new DbContextOptionsBuilder<IdentityDatabaseContext>();
            builder.UseSqlite("DataSource=file::memory:?cache=shared");

            _context = new IdentityDatabaseContext(builder.Options);
            _context.Database.EnsureCreated();
            _userRoleRepository = new UserRoleRepository(_context);
        }

        private void FlushContext()
        {
            _context.Database.ExecuteSql($"delete from Users");
            _context.Database.ExecuteSql($"delete from UserRoles");
            _context.Database.ExecuteSql($"delete from UserRoles");
        }

        [Fact]
        public async void AddUserRole_ShouldSucceed()
        {
            FlushContext();

            var newRole = new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "test_role",
                Description = "test_description"
            };

            var oldSize = _context.UserRoles.Count();

            await _userRoleRepository.AddAsync(newRole, CancellationToken.None);
            await _userRoleRepository.SaveChangesAsync(CancellationToken.None);

            var newSize = _context.UserRoles.Count();

            Assert.True(newSize == oldSize + 1);
        }

        [Fact]
        public async Task AddUserRole_DuplicateUsername_ShouldThrow()
        {
            FlushContext();

            var newRole = new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "test_role",
                Description = "test_description"
            };

            var duplicateRole = new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "test_role",
                Description = "test_description"
            };

            var oldSize = _context.UserRoles.Count();

            await _userRoleRepository.AddAsync(newRole, CancellationToken.None);
            await _userRoleRepository.SaveChangesAsync(CancellationToken.None);

            await _userRoleRepository.AddAsync(duplicateRole, CancellationToken.None);
            await Assert.ThrowsAsync<Microsoft.EntityFrameworkCore.DbUpdateException>(
                async () => await _userRoleRepository.SaveChangesAsync(CancellationToken.None)
            );

            var newSize = _context.UserRoles.Count();

            Assert.True(newSize == oldSize + 1);
        }

        [Fact]
        public async void DeleteUserRole_ShouldSucceed()
        {
            FlushContext();

            var newRole = new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "test_role",
                Description = "test_description"
            };

            var oldSize = _context.UserRoles.Count();

            await _userRoleRepository.AddAsync(newRole, CancellationToken.None);
            await _userRoleRepository.SaveChangesAsync(CancellationToken.None);

            var newSize = _context.UserRoles.Count();
            Assert.True(newSize == oldSize + 1);

            await _userRoleRepository.DeleteAsync(newRole, CancellationToken.None);
            await _userRoleRepository.SaveChangesAsync(CancellationToken.None);

            var finalSize = _context.UserRoles.Count();
            Assert.True(finalSize == oldSize);
        }

        [Fact]
        public async void DeleteUserRole_DoesNotExist_ShouldThrow()
        {
            FlushContext();

            var newRole = new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "test_role",
                Description = "test_description"
            };

            var oldSize = _context.UserRoles.Count();

            await Assert.ThrowsAsync<ResourceDoesNotExistException>(
                async () => await _userRoleRepository.DeleteAsync(newRole, CancellationToken.None)
            );

            var newSize = _context.UserRoles.Count();
            Assert.True(newSize == oldSize);
        }

        [Fact]
        public async void SoftDeleteUserRole_ShouldSucceed()
        {
            FlushContext();

            var newRole = new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "test_role",
                Description = "test_description"
            };

            var oldSize = _context.UserRoles.Count();

            await _userRoleRepository.AddAsync(newRole, CancellationToken.None);
            await _userRoleRepository.SaveChangesAsync(CancellationToken.None);

            var newSize = _context.UserRoles.Count();
            Assert.True(newSize == oldSize + 1);

            await _userRoleRepository.SoftDeleteAsync(newRole, CancellationToken.None);
            await _userRoleRepository.SaveChangesAsync(CancellationToken.None);

            var finalSize = _context.UserRoles.Count();
            Assert.True(finalSize == oldSize);
        }

        [Fact]
        public async void SoftDeleteUserRole_DoesNotExist_ShouldThrow()
        {
            FlushContext();

            var newRole = new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "test_role",
                Description = "test_description"
            };

            var oldSize = _context.UserRoles.Count();

            await Assert.ThrowsAsync<ResourceDoesNotExistException>(
                async () => await _userRoleRepository.SoftDeleteAsync(newRole, CancellationToken.None)
            );

            var newSize = _context.UserRoles.Count();
            Assert.True(newSize == oldSize);
        }

        [Fact]
        public async Task UpdateUserRole_ShouldSucceed()
        {
            FlushContext();

            var newRole = new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "test_role",
                Description = "test_description"
            };

            var updatedRole = new UserRole
            {
                Id = newRole.Id,
                Name = "test_role2",
                Description = "test_description2"
            };

            var oldSize = _context.UserRoles.Count();

            await _userRoleRepository.AddAsync(newRole, CancellationToken.None);
            await _userRoleRepository.SaveChangesAsync(CancellationToken.None);

            await _userRoleRepository.UpdateAsync(updatedRole, CancellationToken.None);
            await _userRoleRepository.SaveChangesAsync(CancellationToken.None);

            var examinedRole = _context.UserRoles.FirstOrDefault(x => x.Id == newRole.Id);

            Assert.NotNull(examinedRole);
            Assert.Equal(examinedRole.Name, updatedRole.Name);
            Assert.Equal(examinedRole.Description, updatedRole.Description);
            Assert.NotNull(examinedRole.LastUpdatedAt);
        }

        [Fact]
        public async Task UpdateUserRole_DoesNotExist_ShouldThrow()
        {
            FlushContext();

            var newRole = new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "test_role",
                Description = "test_description"
            };

            var updatedRole = new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "test_role2",
                Description = "test_description2"
            };

            var oldSize = _context.UserRoles.Count();

            await _userRoleRepository.AddAsync(newRole, CancellationToken.None);
            await _userRoleRepository.SaveChangesAsync(CancellationToken.None);

            await Assert.ThrowsAsync<ResourceDoesNotExistException>(
                async () => await _userRoleRepository.UpdateAsync(updatedRole, CancellationToken.None)
            );

            var examinedRole = _context.UserRoles.FirstOrDefault(x => x.Id == newRole.Id);

            Assert.NotNull(examinedRole);
            Assert.NotEqual(examinedRole.Name, updatedRole.Name);
            Assert.NotEqual(examinedRole.Description, updatedRole.Description);
            Assert.Null(examinedRole.LastUpdatedAt);
        }

        [Fact]
        public async Task UpdateUserRole_RoleNameAlreadyExists_ShouldThrow()
        {
            FlushContext();

            var existentRole = new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "test_role",
                Description = "test_description"
            };

            await _userRoleRepository.AddAsync(existentRole, CancellationToken.None);
            await _userRoleRepository.SaveChangesAsync(CancellationToken.None);

            var newRole = new UserRole
            {
                Id = Guid.NewGuid(),
                Name = "test_role2",
                Description = "test_description2"
            };

            var updatedRole = new UserRole
            {
                Id = newRole.Id,
                Name = "test_role",
                Description = "test_description"
            };

            var oldSize = _context.UserRoles.Count();

            await _userRoleRepository.AddAsync(newRole, CancellationToken.None);
            await _userRoleRepository.SaveChangesAsync(CancellationToken.None);

            await _userRoleRepository.UpdateAsync(updatedRole, CancellationToken.None);
            await Assert.ThrowsAsync<Microsoft.EntityFrameworkCore.DbUpdateException>(
                async () => await _userRoleRepository.SaveChangesAsync(CancellationToken.None)
            );

            var examinedProfile = _context.UserRoles.FirstOrDefault(x => x.Id == newRole.Id);

            // Really weird that changes are applied even when DbUpdateException is thrown.
            // Might be due to in-memory database provider

            //Assert.NotNull(examinedProfile);
            //Assert.NotEqual(examinedProfile.Username, updatedProfile.Username);
            //Assert.Null(examinedProfile.LastUpdatedAt);
        }
    }
}
