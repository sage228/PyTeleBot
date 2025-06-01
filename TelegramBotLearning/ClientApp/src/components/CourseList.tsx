import React from 'react';

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

interface CourseListProps {
    courses: Course[];
}

const CourseList: React.FC<CourseListProps> = ({ courses }) => {
    return (
        <div className="row">
            {courses.map(course => (
                <div key={course.id} className="col-md-4 mb-4">
                    <div className="card h-100">
                        <img src={course.imageUrl} className="card-img-top" alt={course.title} />
                        <div className="card-body">
                            <h5 className="card-title">{course.title}</h5>
                            <p className="card-text">{course.description}</p>
                            <p className="card-text">
                                <small className="text-muted">
                                    Преподаватель: {course.instructor.firstName} {course.instructor.lastName}
                                </small>
                            </p>
                            <p className="card-text">
                                <strong>Цена: {course.price.toLocaleString('ru-RU', { style: 'currency', currency: 'RUB' })}</strong>
                            </p>
                        </div>
                        <div className="card-footer">
                            <a href={`/Course/Details/${course.id}`} className="btn btn-primary">
                                Подробнее
                            </a>
                        </div>
                    </div>
                </div>
            ))}
        </div>
    );
};

export default CourseList; 