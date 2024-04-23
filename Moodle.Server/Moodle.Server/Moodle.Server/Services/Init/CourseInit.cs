namespace Moodle.Server.Services.Init
{
    public class CourseInit
    {
        private readonly DataContext _context;

        public CourseInit(DataContext context)
        {
            _context = context;
        }

        public async Task Init()
        {
            if (!_context.Degrees.Any())
            {
                await _context.Degrees.AddAsync(new Degree { Id = 1, Name = "Gazdaságinformatikus BSc" });
                await _context.Degrees.AddAsync(new Degree { Id = 2, Name = "Mérnökinformatikus BSc" });
                await _context.Degrees.AddAsync(new Degree { Id = 3, Name = "Programtervező Informatikus BSc" });
                await _context.Degrees.AddAsync(new Degree { Id = 4, Name = "Programtervező Informatikus MSc" });
                await _context.Degrees.AddAsync(new Degree { Id = 5, Name = "Adattudomány MSc" });
                await _context.Degrees.AddAsync(new Degree { Id = 6, Name = "Turizmus - vendéglátás BSc" });
                await _context.Degrees.AddAsync(new Degree { Id = 7, Name = "Kereskedelem és marketing BSc" });
                await _context.Degrees.AddAsync(new Degree { Id = 8, Name = "Gazdálkodás és menedzsment BSc" });

                await _context.SaveChangesAsync();
            }

            if (!_context.Courses.Any())
            {
                await _context.Courses.AddAsync(new Course { Id = 1, Name = "Az informatika logikai és algebrai alapjai", Code = "VEMIMAB144IN", Credit = 4});
                await _context.Courses.AddAsync(new Course { Id = 2, Name = "Lineáris algebra", Code = "VEMIMAB344LI", Credit = 4 });
                await _context.Courses.AddAsync(new Course { Id = 3, Name = "Testnevelés I.", Code = "VEPETO1122A", Credit = 0 });
                await _context.Courses.AddAsync(new Course { Id = 4, Name = "Számítógép-architektúrák I.", Code = "VEMIVIB213SF", Credit = 3 });
                await _context.Courses.AddAsync(new Course { Id = 5, Name = "Mesterséges intelligencia alapjai", Code = "VEMISAB254MV", Credit = 4 });
                await _context.Courses.AddAsync(new Course { Id = 6, Name = "A rendszerfejlesztés haladó módszerei", Code = "VEMISAB223RF", Credit = 3 });
                await _context.Courses.AddAsync(new Course { Id = 7, Name = "Programozás II.", Code = "VEMISAB256PF", Credit = 6 });

                await _context.SaveChangesAsync();
            }
        }
    }
}
