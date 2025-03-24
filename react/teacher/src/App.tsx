import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from './components/Header';
import Home from './components/Home';
import Login from './components/Login';
import Courses from './components/Courses';
import LessonDetails from './components/LessonDetails'; // הוספת קומפוננטת עמוד השיעור

const App: React.FC = () => {
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);

    const handleLogin = () => {
        setIsLoggedIn(true);
    };

    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login onLogin={handleLogin} />} />
                <Route path="/courses" element={
                    <>
                        {isLoggedIn && <Header />}
                        <Courses />
                    </>
                } />
                <Route path="/lessons/:id" element={<LessonDetails />} />
            </Routes>
        </Router>
    );
};

export default App;