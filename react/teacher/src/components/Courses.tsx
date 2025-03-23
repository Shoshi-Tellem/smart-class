// TeacherCourses.tsx
import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchCourses, createCourse, editCourse, deleteCourse } from '../stores/courseStore';
import { AppDispatch } from '../stores/store';

interface Course {
    id: number;
    name: string;
    description: string;
}

const Courses: React.FC = () => {
    const dispatch: AppDispatch = useDispatch();
    const courses = useSelector((state: { courses: Course[] }) => state.courses || []); // הוספת || []
    const [courseData, setCourseData] = useState<{ name: string; description: string }>({ name: '', description: '' });
    const [editingId, setEditingId] = useState<number | null>(null);

    useEffect(() => {
        dispatch(fetchCourses());
    }, [dispatch]);

    console.log('Courses:', courses); 

    const handleAddOrUpdate = () => {
        if (editingId) {
            dispatch(editCourse(editingId, { id: editingId, ...courseData }));
        } else {
            dispatch(createCourse(courseData));
        }
        resetForm();
    };

    const resetForm = () => {
        setCourseData({ name: '', description: '' });
        setEditingId(null);
    };

    const handleEdit = (course: Course) => {
        setCourseData({ name: course.name, description: course.description });
        setEditingId(course.id);
    };

    const handleDelete = (id: number) => {
        dispatch(deleteCourse(id));
    };

    return (
        <div>
            <h1>קורסים</h1>
            <input
                type="text"
                placeholder="שם הקורס"
                value={courseData.name}
                onChange={(e) => setCourseData({ ...courseData, name: e.target.value })}
            />
            <input
                type="text"
                placeholder="תיאור הקורס"
                value={courseData.description}
                onChange={(e) => setCourseData({ ...courseData, description: e.target.value })}
            />
            <button onClick={handleAddOrUpdate}>
                {editingId ? 'עדכן קורס' : 'הוסף קורס'}
            </button>
            <ul>
                {courses.length > 0 ? (
                    courses.map((course) => (
                        <li key={course.id}>
                            {course.name} - {course.description}
                            <button onClick={() => handleEdit(course)}>ערוך</button>
                            <button onClick={() => handleDelete(course.id)}>מחק</button>
                        </li>
                    ))
                ) : (
                    <li>אין קורסים זמינים</li>
                )}
            </ul>
        </div>
    );
};

export default Courses;