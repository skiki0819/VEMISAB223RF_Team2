import { useEffect, useState } from 'react';
import axios from 'axios';
import { Navbar } from '../components/Navbar';
import '../styles/Home.css';

interface Course {
    id: string;
    name: string;
    code: string;
    credit: number;
}

export const Home = () => {
    const [courses, setCourses] = useState<Course[]>([]);
    const [filteredCourses, setFilteredCourses] = useState<Course[]>([]);
    const [courseNameFilter, setCourseNameFilter] = useState<string>('');
    const [degreeIdFilter, setDegreeIdFilter] = useState<number | null>(null);

    useEffect(() => {
        const fetchCourses = async () => {
            try {
                const response = await axios.get(`http://localhost:5191/api/Course`);
                if (response.data && response.data.success) {
                    setCourses(response.data.data);
                    setFilteredCourses(response.data.data);
                }
            } catch (error) {
                console.error('Error fetching courses:', error);
            }
        };

        fetchCourses();
    }, []);

    const handleChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;

        if (name === 'courseNameFilter') {
            setCourseNameFilter(value);
            const filtered = courses.filter(course =>
                course.name.toLowerCase().includes(value.toLowerCase())
            );
            setFilteredCourses(filtered);
        } else if (name === 'degreeIdFilter') {
            setDegreeIdFilter(Number(value));
            const filtered = courses.filter(course =>
                course.name.toLowerCase().includes(value.toLowerCase()));
            setFilteredCourses(filtered);

            try {
                const response = await axios.get(`http://localhost:5191/api/Course/GetCoursesByDegree/${value}`);
                if (response.data && response.data.success) {
                    setFilteredCourses(response.data.data);
                    console.log(response.data.data)
                }
            } catch (error) {
                console.error('Error fetching courses by degree:', error);
            }
        }
    };

    return (
        <div>
            <Navbar />
            <h1>Courses</h1>

            <div className ='course-filter'>
                <label htmlFor="courseNameFilter" style={{ fontWeight: 'bold'}}>Filter by Course Name:</label>
                <input
                    type="text"
                    id="courseNameFilter"
                    className="filter"
                    name="courseNameFilter"
                    value={courseNameFilter}
                    onChange={handleChange}
                    placeholder="Enter course name"
                />
            </div>

            <div className ='degree-filter'>
                <label htmlFor="degreeIdFilter" style={{ fontWeight: 'bold'}}>Filter by Degree ID:</label>
                <input
                    type="text"
                    id="degreeIdFilter"
                    className="filter2"
                    name="degreeIdFilter"
                    value={degreeIdFilter ?? ''}
                    onChange={handleChange}
                    placeholder="Enter degree ID"
                />
            </div>

            <div className='course-container'>
                {filteredCourses.map((course) => (
                    <div key={course.id} className='course'>
                        <div className='course-name'>{course.name}</div>
                        <div>{course.code}, Credits: {course.credit}</div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default Home;