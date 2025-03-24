// TeacherCourses.tsx
import React, { useEffect, useState, useCallback } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchCourses, createCourse, editCourse, deleteCourse } from '../stores/courseStore';
import { AppDispatch } from '../stores/store';
import Lessons from './Lessons'; // ייבוא קומפוננטת השיעורים

interface Course {
    id: number;
    name: string;
    description: string;
}

const Courses: React.FC = () => {
    const dispatch: AppDispatch = useDispatch();
    const courses = useSelector((state: { courses: Course[] }) => state.courses || []);
    const [courseData, setCourseData] = useState<{ name: string; description: string }>({ name: '', description: '' });
    const [editingId, setEditingId] = useState<number | null>(null);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);
    const [selectedCourseId, setSelectedCourseId] = useState<number | null>(null); // משתנה לשמירת הקורס שנבחר

    useEffect(() => {
        const fetchData = async () => {
            try {
                console.log('Fetching courses...');
                await dispatch(fetchCourses());
                console.log('Courses fetched successfully:', courses);
                setErrorMessage(null);
            } catch (error) {
                console.error('Error fetching courses:', error);
                const err = error as { response?: { status?: number } };
                if (err.response && err.response.status === 403) {
                    setErrorMessage("אין הרשאה לפעולה זו.");
                } else {
                    setErrorMessage("שגיאה בטעינת הקורסים.");
                }
            }
        };

        fetchData();
    }, [dispatch]);

    const handleAddOrUpdate = async () => {
        try {
            console.log('Course data:', courseData);
            if (editingId) {
                console.log(`Updating course with ID: ${editingId}`);
                await dispatch(editCourse(editingId, { id: editingId, ...courseData }));
            } else {
                console.log('Creating new course');
                await dispatch(createCourse(courseData));
            }
            resetForm();
        } catch (error) {
            console.error('Error during add/update operation:', error);
            setErrorMessage("שגיאה בפעולה.");
        }
    };

    const resetForm = () => {
        setCourseData({ name: '', description: '' });
        setEditingId(null);
        console.log('Form reset');
    };

    const handleEdit = useCallback((course: Course) => {
        console.log('Editing course:', course);
        setCourseData({ name: course.name, description: course.description });
        setEditingId(course.id);
    }, []);

    const handleDelete = useCallback(async (id: number) => {
        try {
            console.log(`Deleting course with ID: ${id}`);
            await dispatch(deleteCourse(id));
        } catch (error) {
            console.error('Error deleting course:', error);
            setErrorMessage("שגיאה במחקת הקורס.");
        }
    }, [dispatch]);

    const handleCourseClick = (courseId: number) => {
        setSelectedCourseId(selectedCourseId === courseId ? null : courseId); // Toggle the selected course
    };

    return (
        <div>
            <h1>קורסים</h1>
            {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
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
                {courses.length > 0 ? 
                (
                    courses.map((course) => (
                        <li key={course.id}>
                            <span onClick={() => handleCourseClick(course.id)} style={{ cursor: 'pointer', textDecoration: 'underline' }}>
                                {course.name} - {course.description}
                            </span>
                            <button onClick={() => handleEdit(course)}>ערוך</button>
                            <button onClick={() => handleDelete(course.id)}>מחק</button>
                            {selectedCourseId === course.id && <Lessons courseId={course.id} />} {/* הצגת השיעורים */}
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