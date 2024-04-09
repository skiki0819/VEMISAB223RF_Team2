import { useEffect, useState } from 'react';
import axios from 'axios';
import { Navbar } from '../components/Navbar';
import '../styles/Home.css';

export const Home = () => {
    const [courses, setCourses] = useState([]);

    useEffect(() => {
        const fetchCourses = async () => {
            try {
                const response = await axios.get(`http://localhost:5191/api/Course`);
                console.log('API válasz:', response.data);
                if (response.data && response.data.isCompletedSuccessfully) {
                    setCourses(response.data.result.data);
                }
            } catch (error) {
                console.error('Error fetching courses:', error);
            }
        };

        fetchCourses();
    }, []);

    return (
        <div>
            <Navbar />
            <h1>Courses</h1>
            <div className='course-container'>
                {courses.map((course: any) => (
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
