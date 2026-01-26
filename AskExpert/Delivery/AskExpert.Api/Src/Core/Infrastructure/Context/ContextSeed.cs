using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AskExperts.Infrastructure.Context
{
    public class ContextSeed
    {
        private static DBContext _dbContext;
        private static IServiceProvider _serviceProvider;

        public static void Seed(DBContext appDbContext, IServiceProvider serviceProvider , IConfiguration configuration)
        {
            _dbContext = appDbContext;
            _dbContext.Database.EnsureCreated();
            _serviceProvider = serviceProvider;

            SeedLK_Specializations();
            SeedLk_RequestStage();

            bool IsSeedData = configuration.GetSection("IsSeedData").Get<bool>();
            if (IsSeedData)
            { SeedExperts();
                SeedQuestion();
            }
        }

        private static void SeedLK_Specializations()
        {
            string LK_SpecializationsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "LK_SpecializationsSeedData.json");
            var LK_Specializations = JsonConvert.DeserializeObject<List<Specialization>>(LK_SpecializationsJson);

            if (LK_Specializations != null && LK_Specializations.Any())
            {
                _dbContext.Database.OpenConnection();
                try
                {
                    if (!_dbContext.Specializations.Any())
                    {
                        foreach (var LK_Specialization in LK_Specializations)
                        {
                            LK_Specialization.CreatedOn = DateTime.Now;
                        }

                        _dbContext.Specializations.AddRange(LK_Specializations);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        var existingSpecializations = _dbContext.Specializations.ToList();
                        var newSpecializations = LK_Specializations
                            .Where(ns => !existingSpecializations.Any(es => es.NameEn == ns.NameEn))
                            .ToList();

                        if (newSpecializations.Any())
                        {
                            foreach (var Specialization in newSpecializations)
                            {
                                Specialization.CreatedOn = DateTime.Now;
                            }

                            _dbContext.Specializations.AddRange(newSpecializations);
                            _dbContext.SaveChanges();
                        }
                    }
                }
                finally
                {
                    _dbContext.Database.CloseConnection();
                }
            }

        }

        private static void SeedLk_RequestStage()
        {

            string Lk_RequestStageJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "Lk_RequestStageSeedData.json");
            var Lk_RequestStages = JsonConvert.DeserializeObject<List<Lk_RequestStage>>(Lk_RequestStageJson);

            if (Lk_RequestStages != null && Lk_RequestStages.Any())
            {
                _dbContext.Database.OpenConnection();
                try
                {
                    _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [4EPlatform.KnowledgeManagement.AskExpert].[dbo].[Lk_RequestStages] ON");
                    if (!_dbContext.Lk_RequestStages.Any())
                    {
                        foreach (var Lk_RequestStage in Lk_RequestStages)
                        {
                            Lk_RequestStage.CreatedOn = DateTime.Now;
                        }

                        _dbContext.Lk_RequestStages.AddRange(Lk_RequestStages);

                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        var existingLk_RequestStages = _dbContext.Lk_RequestStages.ToList();
                        var newLLk_RequestStages = Lk_RequestStages
                            .Where(na => !existingLk_RequestStages.Any(ea => ea.Id == na.Id))
                            .ToList();

                        if (newLLk_RequestStages.Any())
                        {
                            foreach (var Lk_RequestStage in newLLk_RequestStages)
                            {
                                Lk_RequestStage.CreatedOn = DateTime.Now;
                            }

                            _dbContext.Lk_RequestStages.AddRange(newLLk_RequestStages);
                            _dbContext.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _dbContext.Database.CloseConnection();
                }
            }

        }

        private static void SeedExperts()
        {
            string jsonPath = @"SeedData" + Path.DirectorySeparatorChar + "ExpertSeedData.json";
            var experts = JsonConvert.DeserializeObject<List<Expert>>(File.ReadAllText(jsonPath));
            var specializationId = _dbContext.Specializations.Select(x=>x.Id).FirstOrDefault();
            if (experts != null && experts.Any())
            {
                _dbContext.Database.OpenConnection();
                try
                {
                    if (!_dbContext.Experts.Any())
                    {
                        foreach (var expert in experts)
                        {
                            expert.CreatedOn = DateTime.Now;

                            if (expert.Experiences != null)
                            {
                                foreach (var exp in expert.Experiences)
                                {
                                    exp.CreatedOn = DateTime.Now;
                                    exp.SpecializationId = specializationId;
                                }
                            }

                            if (expert.ExpertSpecializations != null)
                            {
                                foreach (var spec in expert.ExpertSpecializations)
                                {
                                    spec.CreatedOn = DateTime.Now;
                                    spec.SpecializationId = specializationId;   
                                }
                            }
                        }

                        _dbContext.Experts.AddRange(experts);
                        _dbContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    // Handle or log the error
                }
                finally
                {
                    _dbContext.Database.CloseConnection();
                }
            }
        }
        private static void SeedQuestion()
        {
            string jsonPath = @"SeedData" + Path.DirectorySeparatorChar + "QuestionSeedData.json";
            var questions = JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText(jsonPath));
            var specializationId = _dbContext.Specializations.Select(x => x.Id).FirstOrDefault();
            var expertId = _dbContext.Experts.Select(x => x.Id).FirstOrDefault();
            if (questions != null && questions.Any())
            {
                _dbContext.Database.OpenConnection();
                try
                {
                    if (!_dbContext.Questions.Any())
                    {
                        foreach (var question in questions)
                        {
                            question.CreatedOn = DateTime.Now;
                            question.SpecializationId = specializationId;
                            if (question.ExpertQuestions != null)
                            {
                                foreach (var expQ in question.ExpertQuestions)
                                {
                                    expQ.CreatedOn = DateTime.Now;
                                    expQ.ExpertId = expertId;
                                }
                            }

                            if (question.Comments != null)
                            {
                                foreach (var comment in question.Comments)
                                {
                                    comment.CreatedOn = DateTime.Now;
                                }
                            }
                        }

                        _dbContext.Questions.AddRange(questions);
                        _dbContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    // log error if needed
                }
                finally
                {
                    _dbContext.Database.CloseConnection();
                }
            }
        }


    }
}
