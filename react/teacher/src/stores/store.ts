// store.ts
import { configureStore } from '@reduxjs/toolkit';
import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux';
import authReducer from './authStore';
import courseReducer from './courseStore'; // ייבוא ה-reducer של קורסים
import lessonReducer from './lessonStore'; // ייבוא ה-reducer של שיעורים

const store = configureStore({
    reducer: {
        auth: authReducer,
        courses: courseReducer,
        lessons: lessonReducer, // הוספת ה-reducer של שיעורים
    },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

// טייפד שימוש ב-dispatch ו-selector
export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;

export default store;