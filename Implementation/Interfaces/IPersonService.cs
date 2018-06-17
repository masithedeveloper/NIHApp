using System;
using System.Collections.Generic;
using NIHApp.Domain.Entities;
using NIHApp.Implementation.Presentation.RestModels;

namespace NIHApp.Implementation.Interfaces
{
	public interface IPersonService
	{
		bool ChangePassword(long personId, string newPassword = "");
		bool IsPasswordReset(long personId);
		bool IsVerified(long personId);
		bool RequireUpgrade(long personId);
		bool MatchVerifyCode(long personId, string hashPassword, int verifyCode);
		PersonModel GetPersonById(long personId);
        Person GetPersonEntityById(long personId);
        PersonModel GetPersonByEmail(string email);
        PersonRegisterModel CreatePerson(PersonRegisterModel personRegisterModel);
        PersonRegisterModel UpgradePerson(PersonRegisterModel personRegisterModel);
        PersonRegisterModel UpdatePerson(PersonRegisterModel personRegisterModel);
        PersonModel UpdatePerson(PersonModel personModel);
		void DeletePerson(long personId);
        IList<PersonModel> GetParentsListByDriverId(long driverId);
        IList<PersonModel> GetPersonByDate(long personId, DateTime modifiedAfter);
	}
}
