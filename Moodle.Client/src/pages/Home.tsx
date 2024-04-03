import { useState, useEffect } from 'react';
import axios from 'axios';

export const Home = () => {
    const [pageNumber] = useState(1);
    const [pageSize] = useState(10);

    useEffect(() => {
        const fetchCourses = async () => {
            try {
                const response = await axios.get(`http://localhost:5191/swagger/v1/swagger.json`);
                console.log('API válasz:', response.data);
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
                
            </ul>
        </div>
    );
};

export default Home;
