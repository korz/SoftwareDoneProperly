using Lesson.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Lesson.Console.Composition
{
    public static class CompositionRoot
    {
        private static IServiceProvider ServiceProvider;

        static CompositionRoot()
        {
            ServiceProvider = Host.CreateDefaultBuilder()
                .ConfigureServices(LoadBindings)
                .Build()
                .Services;
        }

        private static void LoadBindings(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddSingleton<ITeacherRetriever, TeacherRetriever>();
            services.AddSingleton<IStudentRetriever, StudentRetriever>();
            services.AddSingleton<IAssignmentRetriever, AssignmentRetriever>();
            services.AddSingleton<IAverageTeacherGradeCalculator, AverageTeacherGradeCalculator>();
            services.AddSingleton<IGradeAverager, GradeAverager>();
            services.AddSingleton<ILetterGradePercentCalculator, LetterGradePercentCalculator>();
            services.AddSingleton<ITeacherStudentCountRetriever, TeacherStudentCountRetriever>();
        }

        public static T Get<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
