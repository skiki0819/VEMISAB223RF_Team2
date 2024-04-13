import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

export const Footer = () => {
    const [currentTime, setCurrentTime] = useState(new Date().toLocaleTimeString('hu-HU'));

    useEffect(() => {
        const intervalId = setInterval(() => {
            setCurrentTime(new Date().toLocaleTimeString('hu-HU'));
        }, 1000); 

        return () => {
            clearInterval(intervalId); 
        };
    }, []);

    return (
        <div className='Footer'>
            <div className="FooterLinks">
                <Link to="/home"> Home </Link>
            </div>
            <div className="FooterInfo">
                <p style={{ paddingRight: '10px' }}>&copy; Moodle </p>
                <p style={{ fontWeight: 'bold', marginRight: '10px' }}>{currentTime}</p>
            </div>
        </div>
    );
};