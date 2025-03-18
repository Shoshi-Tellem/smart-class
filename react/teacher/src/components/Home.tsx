import '../styles/Home.css'

const Home = () => {
    return (
        <div className="home-container">
            <header className="home-header">
                <h1>ברוכים הבאים למערכת ניהול הלמידה</h1>
                <p>היכנס לאזור האישי שלך כמנהל, מורה או סטודנט</p>
            </header>
            <nav className="home-nav">
                <ul>
                    <li><a href="/login">כניסת מנהל</a></li>
                    <li><a href="/login">כניסת מורה</a></li>
                    <li><a href="/login">כניסת סטודנט</a></li>
                </ul>
            </nav>
            <section className="home-features">
                <h2>תכונות עיקריות</h2>
                <div className="feature">
                    <h3>ניהול קורסים</h3>
                    <p>צור, ערוך ומחק קורסים בקלות.</p>
                </div>
                <div className="feature">
                    <h3>מעקב אחר התקדמות</h3>
                    <p>עקוב אחרי ההתקדמות של התלמידים שלך.</p>
                </div>
                <div className="feature">
                    <h3>משוב מיידי</h3>
                    <p>קבל משוב מיידי על ביצועי התלמידים.</p>
                </div>
            </section>
            <footer className="home-footer">
                <p>© 2025 מערכת ניהול הלמידה. כל הזכויות שמורות.</p>
            </footer>
        </div>
    );
};

export default Home;
