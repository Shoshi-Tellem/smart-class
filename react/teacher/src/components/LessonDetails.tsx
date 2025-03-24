import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../stores/store';
import { fetchFiles, addFile, deleteFile } from '../stores/fileStore'; // נניח שיש לך חנות קבצים

const LessonDetails: React.FC = () => {
    const { lessonId } = useParams<{ lessonId: string }>();
    const dispatch = useAppDispatch();
    const { files, loading, error } = useAppSelector((state: { files: FileState }) => state.files); // ודא שהמאפיין קיים

    const [newFilePath, setNewFilePath] = useState('');
    
    useEffect(() => {
        const fetchData = async () => {
            await dispatch(fetchFiles(Number(lessonId))); // הנח שיש לך פעולה זו
        };
        fetchData();
    }, [dispatch, lessonId]);

    const handleAddFile = async () => {
        await dispatch(addFile({ filePath: newFilePath }));
        setNewFilePath(''); // Reset the input
    };

    const handleDeleteFile = async (id: number) => {
        await dispatch(deleteFile(id));
    };

    return (
        <div>
            <h2>פרטי השיעור {lessonId}</h2>
            {loading && <p>טוען קבצים...</p>}
            {error && <p>שגיאה: {error}</p>}
            <ul>
                {files.map((file) => (
                    <li key={file.id}>
                        {file.filePath}
                        <button onClick={() => handleDeleteFile(file.id)}>מחק</button>
                    </li>
                ))}
            </ul>
            <h3>הוסף קובץ חדש</h3>
            <input 
                type="text" 
                value={newFilePath} 
                onChange={(e) => setNewFilePath(e.target.value)} 
                placeholder="נתיב הקובץ" 
            />
            <button onClick={handleAddFile}>הוסף קובץ</button>
        </div>
    );
};

export default LessonDetails;