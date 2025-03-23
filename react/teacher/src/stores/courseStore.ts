import { configureStore, createSlice, PayloadAction } from '@reduxjs/toolkit';
import apiClient from '../extensions/apiService';

interface Course {
    id: number;
    name: string;
    description: string;
}

interface NewCourse {
    name: string;
    description: string;
}

const courseSlice = createSlice({
    name: 'courses',
    initialState: [] as Course[],
    reducers: {
        setCourses: (state, action: PayloadAction<Course[]>) => action.payload,
        addCourse: (state, action: PayloadAction<Course>) => {
            state.push(action.payload);
        },
        updateCourse: (state, action: PayloadAction<Course>) => {
            const index = state.findIndex(course => course.id === action.payload.id);
            if (index !== -1) {
                state[index] = action.payload;
            }
        },
        removeCourse: (state, action: PayloadAction<number>) => {
            return state.filter(course => course.id !== action.payload);
        },
    },
});

export const { setCourses, addCourse, updateCourse, removeCourse } = courseSlice.actions;

export const fetchCourses = () => async (dispatch: any) => {
    const response = await apiClient.get<Course[]>('teacher/myCourses'); // עדכון הכתובת ל/myCourses
    dispatch(setCourses(response.data));
};

export const createCourse = (course: NewCourse) => async (dispatch: any) => {
    const response = await apiClient.post<Course>('/course', course);
    dispatch(addCourse(response.data));
    console.log('Course added:', response.data);
};

export const editCourse = (id: number, course: Course) => async (dispatch: any) => {
    const response = await apiClient.put<Course>(`/course/${id}`, course);
    dispatch(updateCourse(response.data));
};

export const deleteCourse = (id: number) => async (dispatch: any) => {
    await apiClient.delete(`/course/${id}`);
    dispatch(removeCourse(id));
};

const courseStore = configureStore({
    reducer: {
        courses: courseSlice.reducer,
    },
});

export default courseStore;