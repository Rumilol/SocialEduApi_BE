using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SocialEduApi.Models.Entities;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Data
{
    public class DbInitializer
    {
        private const string STUDENT_EMAIL = "student@gmaill.com";
        private const string PROF_EMAIL = "prof@gmaill.com";

        public static async Task Initialize(UserManager<ApplicationUser> userManager,
                                            ApplicationDbContext context,
                                            RoleManager<IdentityRole> roleManager)
        {
            if (context.AspNetRoles.Any()) { return; }
            await CreateRoles(context, roleManager);
            await CreateDefaultUsers(userManager, context);
            await InitializeData(context, userManager);
            await GenerateData(context, userManager);
        }

        private static async Task CreateRoles(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            if (context.AspNetRoles.Any()) return;
            var a = await roleManager.CreateAsync(new IdentityRole() { Name = "admin" });
            await roleManager.CreateAsync(new IdentityRole() { Name = "user" });

            await context.InstitutionRoles.AddAsync(new InstitutionRole() { Id = (int)InstitutionRole.Roles.Student, Name = "Student" });
            await context.InstitutionRoles.AddAsync(new InstitutionRole() { Id = (int)InstitutionRole.Roles.Professor, Name = "Profesor" });
            await context.InstitutionRoles.AddAsync(new InstitutionRole() { Id = (int)InstitutionRole.Roles.Admin, Name = "Administrator" });

            await context.SaveChangesAsync();
        }

        private static async Task CreateDefaultUsers(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            var existingStudent = await userManager.FindByEmailAsync(STUDENT_EMAIL);
            if (existingStudent == null)
            {
                ApplicationUser user = new()
                {
                    UserName = STUDENT_EMAIL,
                    FirstName = "Mihael",
                    LastName = "Ladić",
                    Gender = "M",
                    Location = "Rijeka",
                    Description = "Student na Fakultetu informatike i digitalnih tehnologija u Rijeci.",
                    Image = "https://primefaces.org/cdn/primeng/images/demo/avatar/onyamalimba.png",
                    UserpageBackground = "https://primefaces.org/cdn/primeng/images/demo/avatar/onyamalimba.png",
                    Email = STUDENT_EMAIL,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    General = "General 1;;General 2;;General 3",
                    Interests = "Interes 1;;Interes 2;;Interes 3",
                    Education = "Prijediplomski studij Informatike na Fakultetu informatike i digitalnih tehnologija u Rijeci",
                    Experience = "Junior web developer, Kompanija 1, 2019 - .",
                    Links = "https://www.google.com/",
                    DateOfBirth = DateTime.Now.AddYears(-22).AddMonths(-3),
                    JoinedDate = DateTime.Now.AddYears(-10).AddMonths(-3),

                };
                var a = await userManager.CreateAsync(user);
                var b = await userManager.AddPasswordAsync(user, "password");
                var c = await userManager.AddToRoleAsync(user, "user");
                context.SaveChanges();
            }

            var existingProf = await userManager.FindByEmailAsync(PROF_EMAIL);
            if (existingProf == null)
            {
                ApplicationUser user = new()
                {
                    UserName = PROF_EMAIL,
                    FirstName = "Luka",
                    LastName = "Mavrić",
                    Gender = "M",
                    Location = "Rijeka",
                    Description = "10 godina na Fakultetu informatike i digitalnih tehnologija, prethodno Odjel za informatiku.",
                    Image = "https://primefaces.org/cdn/primeng/images/demo/avatar/amyelsner.png",
                    UserpageBackground = "https://primefaces.org/cdn/primeng/images/demo/avatar/amyelsner.png",
                    Email = PROF_EMAIL,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    General = "General 1;;General 2;;General 3",
                    Interests = "Interes 1;;Interes 2;;Interes 3",
                    Education = "Prijediplomski i diplomski studij Informatike na Fakultetu informatike i digitalnih tehnologija u Rijeci",
                    Experience = "Junior web developer, Kompanija 1, 2018 - .",
                    Links = "https://www.google.com/",
                    DateOfBirth = DateTime.Now.AddYears(-24),
                    JoinedDate = DateTime.Now.AddYears(-5).AddMonths(-6),
                };
                var a = await userManager.CreateAsync(user);
                var b = await userManager.AddPasswordAsync(user, "password");
                var c = await userManager.AddToRoleAsync(user, "admin");
                context.SaveChanges();
            }
        }

        public static async Task InitializeData(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            var student = await userManager.FindByEmailAsync(STUDENT_EMAIL);
            var prof = await userManager.FindByEmailAsync(PROF_EMAIL);

            var institution = new Institution()
            {
                Name = "Fakultet informatike i digitalnih tehnologija rijeka",
                Abbreviation = "FIDiT",
                Description = "Fakultet informatike i digitalnih tehnologija i Rijeci",
                Image = "data:image/jpeg;base64,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",
                PageBackground = "data:image/jpeg;base64,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"
            };
            context.Institutions.Add(institution);
            await context.SaveChangesAsync();

            var userInstitution = new UserInstitutionBelongs()
            {
                UserID = student!.Id,
                Institution = institution,
                RoleID = (int)InstitutionRole.Roles.Student
            };
            var userInstitutionProf = new UserInstitutionBelongs()
            {
                UserID = prof!.Id,
                Institution = institution,
                RoleID = (int)InstitutionRole.Roles.Professor
            };
            await context.UserInstitutionBelongses.AddAsync(userInstitutionProf);
            await context.UserInstitutionBelongses.AddAsync(userInstitution);
            await context.SaveChangesAsync();

            var subject = new Subject()
            {
                Name = "Elektroničko poslovanje i digitalne tehnologije",
                Abbreviation = "EPIDI",
                Description = "Cilj predmeta je usvajanje temeljnih i proširenih znanja iz područja elektroničkog poslovanja i upravljanja\r\ndigitalnim inovacijama. Ta znanja, između ostalog, uključuju analizu tržišta u kontekstu upotrebe\r\nproizvoda informacijsko-komunikacijske tehnologije, vrednovanje procesa upravljanja IKT inovacijama u\r\nposlovanju, upravljanje inovacijom i izradu poslovnog plana te izradu prijedloga dizajna sustava za\r\nelektroničko poslovanje.",
                PageBackground = "https://wallpapers.com/images/hd/cool-gray-61nfwad1bullevu4.jpg",
                Institution = institution,
                CreatedDate = DateTime.Now.AddMonths(-4)
            };
            context.Subjects.Add(subject);
            await context.SaveChangesAsync();

            var userOnSubject = new UserOnSubject()
            {
                UserID = student!.Id,
                SubjectID = subject.Id,
                HasAdminRights = false,
                JoinedDate = DateTime.Now
            };
            var userOnSubjectProf = new UserOnSubject()
            {
                UserID = prof!.Id,
                SubjectID = subject.Id,
                HasAdminRights = true,
                JoinedDate = DateTime.Now.AddMonths(-2)
            };
            context.UsersOnSubjects.Add(userOnSubject);
            context.UsersOnSubjects.Add(userOnSubjectProf);
            await context.SaveChangesAsync();

            var forum = new Forum()
            {
                Name = "1. forum rasprava - razvoj IKT, privatnost i društvene mreže",
                Description = "Dvije prezentacije sa smjernicama za prvu forum raspravu postavljene su u bloku FORUM RASPRAVE (1. Razvoj IKT te 1. Privatnost i društvene mreže). \r\n\r\nMolim vas proučite prezentacije, pogledajte videozapise, pronađite i neke vlastite izvore informacija i perspektiva te raspravite o nekoliko odabranih tema iz videa i/ili tema/točaka/pitanja u prezentacijama, odnosno dajte svoj osvrt/mišljenje. ",
                Subject = subject,
                CreatedDate = DateTime.Now.AddMonths(-1)
            };
            context.Forums.Add(forum);
            await context.SaveChangesAsync();

            var forumPost = new ForumPost()
            {
                Forum = forum,
                UserID = student!.Id,
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In suscipit viverra ullamcorper. Proin fermentum mauris et dolor faucibus suscipit. Vestibulum a enim aliquam, accumsan libero sed, imperdiet lorem. Donec maximus consequat turpis, a finibus mi egestas non. Morbi elit neque, dapibus quis venenatis eu, mattis id mauris. Duis non facilisis risus. Sed non ligula nec ex sagittis convallis. Morbi in porta justo. Duis non scelerisque magna, sit amet malesuada ante. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Duis eu nisl facilisis, condimentum massa et, cursus lectus.",
                CreatedDate = DateTime.Now
            };
            context.ForumPosts.Add(forumPost);
            await context.SaveChangesAsync();

            var projectTask = new ProjectTask()
            {
                Subject = subject,
                Title = "Startup projekt",
                Description = "Tijekom semestra studenti će u timu izraditi tehnološko rješenje (inovativnu web i mobilnu aplikaciju na zadanu temu) i startup poslovni plan za njega, na temelju dobivenih uputa, nastavnih materijala i definiranih zadataka.",
                Criteria = "30 - razrađenost poslovnog plana\n20 - Dizajn\n20 - Aplikacija",
                MaxGrade = 70,
                CreatedDate = DateTime.Today.AddMonths(-2)
            };
            context.ProjectTasks.Add(projectTask);
            await context.SaveChangesAsync();

            var projectSubmission = new ProjectSubmission()
            {
                Subject = subject,
                ProjectTask = projectTask,
                UserID = student!.Id,
                Title = "Linmer Networking",
                Description = "Sustav za dijeljenje znanja i čuvanje podataka o uspješnosti tijekom studiranja",
                ImageLink = "https://code.visualstudio.com/assets/docs/languages/typescript/overview.png",
                Grade = 70,
                UploadDate = DateTime.Now.AddDays(-4)
            };
            context.ProjectSubmissions.Add(projectSubmission);
            await context.SaveChangesAsync();

            var highlightsFolder = new SubmissionFolder()
            {
                UserID = student!.Id,
                Name = "Project management",
                Description = ""
            };
            context.SubmissionFolders.Add(highlightsFolder);
            await context.SaveChangesAsync();

            var highlightsFolderContains = new SubmissionFolderContains()
            {
                Submission = projectSubmission,
                Folder = highlightsFolder
            };
            context.SubmissionFolderContainses.Add(highlightsFolderContains);
            await context.SaveChangesAsync();

            var userFolder = new SavedUsersFolder()
            {
                UserID = student!.Id,
                Name = "Provjeriti za praksu",
            };
            context.SavedUsersFolders.Add(userFolder);
            await context.SaveChangesAsync();

            var savedUser = new SavedUser()
            {
                Folder = userFolder,
                UserID = prof.Id
            };
            context.SavedUsers.Add(savedUser);
            await context.SaveChangesAsync();
        }

        private static async Task GenerateData(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            const int userCount = 20;
            const int institutionCount = 6;
            const int institutionBelongsCount = 9;
            const int subjectCount = 3;
            const int subjectMemberCount = 10;
            const int forumCount = 5;
            const int forumPostCount = 20;
            const int projectTaskCount = 5;
            const int projectSubmissionCount = 20;
            const int submissionFolderPerUser = 5;
            const int submissionsPerFolder = 5;
            const int userFolderPerUser = 5;
            const int usersPerFolder = 5;

            Random random = new Random();

            var prof = context.Users.FirstOrDefault(p => p.Email == PROF_EMAIL);
            var student = context.Users.FirstOrDefault(p => p.Email == STUDENT_EMAIL);
            var subjectEPIDI = context.Subjects.FirstOrDefault(p => p.Abbreviation == "EPIDI");

            //ApplicationUsers
            var userFaker = ApplicationUser.GetFaker();
            var users = userFaker.Generate(userCount);
            foreach (var userData in users)
            {
                userData.UserName = userData.Email;
                var b = userManager.CreateAsync(userData, "password");
            }
            context.SaveChanges();
            var userIDs = context.Users.Select(p => p.Id).ToList();

            //Institutions
            var institutionFaker = Institution.GetFaker();
            var institutions = institutionFaker.Generate(institutionCount);
            context.Institutions.AddRange(institutions);
            context.SaveChanges();
            institutions = context.Institutions.ToList();
            //var institutionIDs = institutions.Select(p => p.Id).ToList();

            foreach (var institution in institutions)
            {
                //UserInstitutionBelongs
                var instBelongsFaker = UserInstitutionBelongs.GetFaker(userIDs, institution.Id);
                var belongses = instBelongsFaker.Generate(institutionBelongsCount);

                //remove duplicates
                var groups = belongses.GroupBy(p => p.UserID);
                foreach (var group in groups)
                {
                    if (group.Count() > 1) belongses.RemoveAll(p => p.UserID == group.Key);
                }

                context.UserInstitutionBelongses.AddRange(belongses);
                var institutionUserIDs = belongses.Select(p => p.UserID).ToList();

                //Subject
                var subjectFaker = Subject.GetFaker(institution.Id);
                var subjects = subjectFaker.Generate(subjectCount);
                context.Subjects.AddRange(subjects);
                context.SaveChanges();
                subjects = context.Subjects.Where(p => p.InstitutionID == institution.Id).ToList();

                foreach (var subject in subjects)
                {
                    //UserOnSubject
                    var UOSfaker = UserOnSubject.GetFaker(institutionUserIDs, subject.Id);
                    var sMembers = UOSfaker.Generate(subjectMemberCount);

                    //remove duplicates
                    var sGroups = sMembers.GroupBy(p => p.UserID);
                    foreach (var group in sGroups)
                    {
                        if (group.Count() > 1)
                        {
                            var groupList = group.ToList();
                            var first = group.First();
                            groupList.Remove(first);
                            foreach (var toRemove in groupList)
                            {
                                sMembers.Remove(toRemove);
                            }
                        }
                    }
                    sMembers.RemoveAll(p => p.SubjectID == subjectEPIDI.Id && (p.UserID == prof.Id || p.UserID == student.Id));
                    context.UsersOnSubjects.AddRange(sMembers);
                    var memberUserIDs = sMembers.Select(p => p.UserID).ToList();

                    //Forum
                    var forumFaker = Forum.GetFaker(subject.Id);
                    var forums = forumFaker.Generate(forumCount);
                    context.Forums.AddRange(forums);

                    //ForumPost
                    var fPostFaker = ForumPost.GetFaker(forums, memberUserIDs);
                    var posts = fPostFaker.Generate(forumPostCount);
                    context.ForumPosts.AddRange(posts);

                    //ProjectTask
                    var pTaskFaker = ProjectTask.GetFaker(subject.Id);
                    var pTasks = pTaskFaker.Generate(projectTaskCount);
                    context.ProjectTasks.AddRange(pTasks);

                    //ProjectSubmission
                    var pSubmissionFaker = ProjectSubmission.GetFaker(pTasks, memberUserIDs, subject.Id);
                    var pSubmissions = pSubmissionFaker.Generate(projectSubmissionCount);
                    var pSubGroups = pSubmissions.GroupBy(p => p.UserID + p.ProjectTaskID);
                    foreach (var subGroup in pSubGroups)
                    {
                        if (subGroup.Count() > 1)
                        {
                            var list = subGroup.ToList();
                            var first = list.First();
                            list.Remove(first);
                            foreach (var submission in list)
                            {
                                pSubmissions.Remove(submission);
                            }
                        }
                    }
                    context.ProjectSubmissions.AddRange(pSubmissions);
                }
            }
            context.SaveChanges();

            var submissionIDs = context.ProjectSubmissions.Select(p => p.Id).ToList();

            foreach (string userID in userIDs)
            {
                //SubmissionFolder
                var subFolderFaker = SubmissionFolder.GetFaker(userID);
                var folders = subFolderFaker.Generate(submissionFolderPerUser);
                context.SubmissionFolders.AddRange(folders);
                foreach (var folder in folders)
                {
                    //SubmissionFolderContains
                    var folderSubmissionFaker = SubmissionFolderContains.GetFaker(submissionIDs, folder);
                    var folderSubmissions = folderSubmissionFaker.Generate(submissionsPerFolder);
                    var groups = folderSubmissions.GroupBy(p => p.SubmissionID);
                    //removing duplicates
                    foreach (var group in groups)
                    {
                        if (group.Count() > 1)
                        {
                            var list = group.ToList();
                            var first = list.First();
                            list.Remove(first);
                            foreach (var submission in list)
                            {
                                folderSubmissions.Remove(submission);
                            }
                        }
                    }
                    context.SubmissionFolderContainses.AddRange(folderSubmissions);
                }
                //SavedUsersFolder
                var userFolderFaker = SavedUsersFolder.GetFaker(userID);
                var userFolders = userFolderFaker.Generate(userFolderPerUser);
                context.SavedUsersFolders.AddRange(userFolders);
                var notMe = userIDs.Where(p => p != userID);
                foreach (var folder in userFolders)
                {
                    //SavedUser
                    var savedUsersFaker = SavedUser.GetFaker(notMe, folder);
                    var savedUsers = savedUsersFaker.Generate(usersPerFolder);
                    var groups = savedUsers.GroupBy(p => p.UserID);
                    //removing duplicates
                    foreach (var group in groups)
                    {
                        if (group.Count() > 1)
                        {
                            var list = group.ToList();
                            var first = list.First();
                            list.Remove(first);
                            foreach (var submission in list)
                            {
                                savedUsers.Remove(submission);
                            }
                        }
                    }
                    context.SavedUsers.AddRange(savedUsers);
                }
            }
            context.SaveChanges();
        }
    }
}
