import { useState, useEffect } from 'react';
import axios from 'axios';

interface Course {
    id: number;
    name: string;
    code: string;
    credit: number;
}

export const Home = () => {
    const [courses, setCourses] = useState<Course[]>([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    useEffect(() => {
        const fetchCourses = async () => {
            try {
                const response = await axios.get('http://localhost:5191/Models/Course/courses?pageNumber=${pageNumber}&pageSize=${pageSize}');
                console.log('API válasz:', response.data);
                setCourses(response.data);
            } catch (error) {
                console.error('Error fetching courses:', error);
            }
        };

        fetchCourses();
    }, [pageNumber, pageSize]);

    return (
        <div>
            <h1>Courses</h1>
            <ul>
                {courses.map(course => (
                    <li key={course.id}>
                        <h2>{course.name}</h2>
                        <p>Code: {course.code}</p>
                        <p>Credit: {course.credit}</p>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Home;
