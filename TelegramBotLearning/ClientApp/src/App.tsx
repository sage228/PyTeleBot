import React, { useState, useEffect } from 'react';
import CourseList from './components/CourseList';
import Login from './components/Login';

interface Course {
    id: number;
    title: string;
    description: string;
    imageUrl: string;
    price: number;
    instructor: {
        firstName: string;
        lastName: string;
    };
}

const App: React.FC = () => {
    const [courses, setCourses] = useState<Course[]>([]);
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        fetchCourses();
    }, []);

    const fetchCourses = async () => {
        try {
            const response = await fetch('/api/courses');
            const data = await response.json();
            setCourses(data);
        } catch (error) {
            console.error('Error fetching courses:', error);
        }
    };

    const handleLogin = async (email: string, password: string) => {
        try {
            const response = await fetch('/api/account/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ email, password }),
            });

            if (response.ok) {
                setIsAuthenticated(true);
            } else {
                alert('Ошибка входа. Проверьте email и пароль.');
            }
        } catch (error) {
            console.error('Login error:', error);
            alert('Произошла ошибка при входе.');
        }
    };

    return (
        <div className="container mt-4">
            {!isAuthenticated ? (
                <Login onLogin={handleLogin} />
            ) : (
                <>
                    <h1 className="text-center mb-4">Курсы по созданию Telegram ботов на Python</h1>
                    <CourseList courses={courses} />
                </>
            )}
        </div>
    );
};

export default App; 