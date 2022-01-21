using EfExp.Models;

namespace EfExp.ModelFactories
{
	public class PostFactory : IEntityFactory<Post>
	{
		public Post Create()
		{
			var createdDate = DateTime.Now.AddDays(-Faker.RandomNumber.Next(10, 3000));

			return new Post
			{
				Title = Faker.Lorem.Sentence(30),
				Slug = Faker.Internet.Url(),
				Description = Faker.Lorem.Paragraph(),
				ContentBody = string.Join(Environment.NewLine, Faker.Lorem.Paragraphs(5)),
				Author = Faker.Name.FullName(),
				CreatedDate = createdDate,
				PublishDate = createdDate.AddDays(Faker.RandomNumber.Next(1, 35))
			};
		}

		public IEnumerable<Post> Create(int n)
		{
			return Enumerable.Range(1, n)
				.Select(x => Create());
		}
	}
}