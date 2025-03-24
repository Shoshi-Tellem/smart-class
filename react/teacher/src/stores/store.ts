// store.ts
import { configureStore } from '@reduxjs/toolkit';
import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux';
import authReducer from './authStore';
import courseReducer from './courseStore'; // ייבוא ה-reducer של קורסים
import lessonReducer from './lessonStore'; // ייבוא ה-reducer של שיעורים
import fileReducer from './fileStore'; // ייבוא ה-reducer של קבצים

const store = configureStore({
    reducer: {
        auth: authReducer,
        courses: courseReducer,
        lessons: lessonReducer, // הוספת ה-reducer של שיעורים
        files: fileReducer, // הוספת ה-reducer של קבצים
    },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

// טייפד שימוש ב-dispatch ו-selector
export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;

export default store;