import { useEffect } from 'react';
import axios from 'axios';

export const Home = () => {

    useEffect(() => {
        const fetchCourses = async () => {
            try {
                const response = await axios.get(`http://localhost:5191/api/Course`);
                console.log('API válasz:', response.data);
            } catch (error) {
                console.error('Error fetching courses:', error);
            }
        };
        

        fetchCourses();
    }, []);

    return (
        <div>
            <h1>Courses</h1>
            <ul>
                
            </ul>
        </div>
    );
};

export default Home;
