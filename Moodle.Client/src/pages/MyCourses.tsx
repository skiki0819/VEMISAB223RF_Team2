import { useEffect, useState } from 'react';
import axios from 'axios';
import Modal from 'react-modal';
import { Navbar } from '../components/Navbar';
import { Footer } from "../components/Footer";
import '../styles/Home.css';

interface Course {
    id: string;
    name: string;
    code: string;
    credit: number;
}

export const MyCourses = () => {
    const userId = localStorage.getItem('userId');
    const roleId = localStorage.getItem('roleId');
    const [courses, setCourses] = useState<Course[]>([]);
    const [modalIsOpen, setModalIsOpen] = useState(false);
    const [eventName, setEventName] = useState('');
    const [eventDescription, setEventDescription] = useState('');
    const [selectedCourseId, setSelectedCourseId] = useState<string | null>(null); // Kiválasztott kurzus azonosítója

    useEffect(() => {
        const fetchCourses = async () => {
            try {
                if (userId) {
                    const response = await axios.get(`http://localhost:5191/api/User/GetCoursesByUser/${userId}`);
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

    const handleCreateEvent = async () => {
        if (!selectedCourseId || !eventName || !eventDescription) {
            console.error('Missing data');
            return;
        }

        try {
            const eventInfo = {
                courseId: selectedCourseId,
                name: eventName,
                description: eventDescription,
            };

            const response = await axios.post('http://localhost:5191/api/Event/CreateEvent', eventInfo);
            if (response.data && response.data.success) {
                closeModal();
                console.log('Event created successfully');
            }
        } catch (error) {
            console.error('Error creating event:', error);
        }
    };

    const openModal = (courseId: string) => {
        setSelectedCourseId(courseId);
        setModalIsOpen(true);
    };

    const closeModal = () => {
        setModalIsOpen(false);
        setSelectedCourseId(null);
    };

    return (
        <div>
            <Navbar />
            <h2>My Courses</h2>
            <div className='course-container'>
                {courses.map((course) => (
                    <div key={course.id} className='course'>
                        <div className='course-name'>{course.name}</div>
                        <div>{course.code}, Credits: {course.credit}</div>
                        {roleId === '2' && (
                            <button onClick={() => openModal(course.id)}>Create Event</button>
                        )}
                    </div>
                ))}
            </div>
            <Footer />

            <Modal
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                contentLabel="Create Event Modal"
            >
                <h2>Create Event</h2>
                <div className='modalInput'>
                    <input type="text" placeholder="Name" value={eventName} onChange={(e) => setEventName(e.target.value)} /> 
                </div><br></br>
                <div className='modalInput'>
                    <input type="text" placeholder="Description" value={eventDescription} onChange={(e) => setEventDescription(e.target.value)} />
                </div><br></br>
                <button onClick={handleCreateEvent}>Create</button>
                <button onClick={closeModal}>Cancel</button>
            </Modal>
        </div>
    );
};

export default MyCourses;
