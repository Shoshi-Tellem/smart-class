import React, { useEffect, useState } from 'react';
import { useAppDispatch, useAppSelector } from '../stores/store'; 
import { fetchLessons, addLesson, updateLesson, deleteLesson } from '../stores/lessonStore'; 

interface Lesson {
    id: number;
    name: string;
    courseId: number; // הוספת מזהה הקורס
}

interface LessonState {
    lessons: Lesson[];
    loading: boolean;
    error: string | null;
}

interface LessonsProps {
    courseId: number; // מזהה הקורס יישלח מהפרופס
}

const Lessons: React.FC<LessonsProps> = ({ courseId }) => {
    const dispatch = useAppDispatch();
    const { lessons = [], loading, error } = useAppSelector((state: { lessons: LessonState }) => state.lessons || { lessons: [] });
    const [newLessonName, setNewLessonName] = useState(''); // רק שם השיעור
    const [editLesson, setEditLesson] = useState<Lesson | null>(null);

    useEffect(() => {
        const fetchData = async () => {
            await dispatch(fetchLessons(courseId));
        };
        fetchData();
    }, [dispatch, courseId]);

    const handleAddLesson = async () => {
        const lessonToAdd = { name: newLessonName, courseId }; // הוספת שם השיעור ומזהה הקורס
        await dispatch(addLesson(lessonToAdd)); 
        setNewLessonName(''); // Reset the input
    };

    const handleUpdateLesson = async () => {
        if (editLesson) {
            await dispatch(updateLesson({ id: editLesson.id, lesson: editLesson })); // Pass the correct object
            setEditLesson(null); // Reset the edit state
        }
    };

    const handleDeleteLesson = async (id: number) => {
        await dispatch(deleteLesson(id));
    };

    return (
        <div>
            <h2>שיעורים לקורס {courseId}</h2>
            {loading && <p>טוען שיעורים...</p>}
            {error && <p>שגיאה: {error}</p>}
            <ul>
                {Array.isArray(lessons) && lessons.length > 0 ? (
                    lessons.map((lesson) => (
                        <li key={lesson.id}>
                            {lesson.name}
                            <button onClick={() => setEditLesson(lesson)}>ערוך</button>
                            <button onClick={() => handleDeleteLesson(lesson.id)}>מחק</button>
                        </li>
                    ))
                ) : (
                    <li>אין שיעורים זמינים לקורס זה</li>
                )}
            </ul>
            <h3>הוסף שיעור חדש</h3>
            <input 
                type="text" 
                value={newLessonName} 
                onChange={(e) => setNewLessonName(e.target.value)} 
                placeholder="שם השיעור" 
            />
            <button onClick={handleAddLesson}>הוסף שיעור</button>

            {editLesson && (
                <div>
                    <h3>ערוך שיעור</h3>
                    <input 
                        type="text" 
                        value={editLesson.name} 
                        onChange={(e) => setEditLesson({ ...editLesson, name: e.target.value })} 
                    />
                    <button onClick={handleUpdateLesson}>שמור שינויים</button>
                </div>
            )}
        </div>
    );
};

export default Lessons;