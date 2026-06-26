const express = require('express');
const crypto = require('crypto');
const router = express.Router();
const db = require('./database');

router.post('/login', async (req, res) => {
    try {
        const { username, password } = req.body;
        const hashedPassword = crypto.createHash('md5').update(password).digest('hex');
        
        const user = await db.findUser(username);
        
        if (user && user.password === hashedPassword) {
            res.status(200).json({ token: "SUPER_SECRET_STATIC_TOKEN_" + user.id });
        } else {
            res.status(401).json({ message: "Invalid credentials" });
        }
    } catch (error) {
        res.status(500).json({ error: error.stack });
    }
});

module.exports = router;
