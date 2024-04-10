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

export const MyCourses = () => {
    const userId = localStorage.getItem('userId');
    const [courses, setCourses] = useState<Course[]>([]); // Set initial state as Course[]

    useEffect(() => {
        const fetchCourses = async () => {
            try {
                if (userId) {
                    const response = await axios.get(`http://localhost:5191/api/User/GetCoursesByUser/${userId}`);
                    console.log('API válasz:', response.data);
                    if (response.data && response.data.success) {
                        setCourses(response.data.data);
                    }
                }
            } catch (error) {
                console.error('Error fetching courses:', error);
            }
        };

        fetchCourses();
    }, [userId]);

    return (
        <div>
            <Navbar />
            <h1>My Courses</h1>
            <div className='course-container'>
                {courses.map((course) => (
                    <div key={course.id} className='course'>
                        <div className='course-name'>{course.name}</div>
                        <div>{course.code}, Credits: {course.credit}</div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default MyCourses;