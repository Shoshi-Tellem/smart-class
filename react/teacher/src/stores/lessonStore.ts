import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';

interface Lesson {
    id: number;
    name: string;
    description: string;
    courseId: number;
}

interface LessonPostPut {
    courseId: number; 
    name: string; 
}

interface LessonState {
    lessons: Lesson[];
    loading: boolean;
    error: string | null;
}

// אקשן אסינכרוני להבאת שיעורים לפי courseId
export const fetchLessons = createAsyncThunk<Lesson[], number>(
    'lessons/fetchLessons',
    async (courseId) => {
        const response = await axios.get(`https://localhost:7253/api/Lesson/myLessons?courseId=${courseId}`);
        return response.data; 
    }
);

// אקשן אסינכרוני להוספת שיעור
export const addLesson = createAsyncThunk<Lesson, LessonPostPut>(
    'lessons/addLesson',
    async (lessonPost) => {
        console.log('lessonPost', lessonPost);
        const response = await axios.post('https://localhost:7253/api/Lesson', lessonPost);
        return response.data; 
    }
);

// אקשן אסינכרוני לעדכון שיעור
export const updateLesson = createAsyncThunk<Lesson, { id: number; lesson: Partial<Lesson> }>(
    'lessons/updateLesson',
    async ({ id, lesson }) => {
        const response = await axios.put(`https://localhost:7253/api/Lesson/${id}`, lesson);
        return response.data;
    }
);

// אקשן אסינכרוני למחיקת שיעור
export const deleteLesson = createAsyncThunk<number, number>(
    'lessons/deleteLesson',
    async (id) => {
        await axios.delete(`https://localhost:7253/api/Lesson/${id}`);
        return id;
    }
);

// יצירת סלאיס עבור שיעורים
const lessonSlice = createSlice({
    name: 'lessons',
    initialState: { lessons: [], loading: false, error: null } as LessonState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchLessons.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(fetchLessons.fulfilled, (state, action) => {
                state.loading = false;
                state.lessons = action.payload;
            })
            .addCase(fetchLessons.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message || 'שגיאה בטעינת שיעורים';
            })
            .addCase(addLesson.fulfilled, (state, action) => {
                state.lessons.push(action.payload);
            })
            .addCase(updateLesson.fulfilled, (state, action) => {
                const index = state.lessons.findIndex(lesson => lesson.id === action.payload.id);
                if (index !== -1) {
                    state.lessons[index] = action.payload;
                }
            })
            .addCase(deleteLesson.fulfilled, (state, action) => {
                state.lessons = state.lessons.filter(lesson => lesson.id !== action.payload);
            });
    },
});

// ייצוא רדוסר
export default lessonSlice.reducer;