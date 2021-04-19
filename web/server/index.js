const express = require('express')
const cors = require('cors')
const mongoose = require('mongoose')
const arquivoRoute = require('./routes/arquivo')
const app = express()

require('dotenv').config()
const port = process.env.PORT

app.use(cors())
app.use(express.json())
app.use(express.static(__dirname + '/public/'))

mongoose.connect(process.env.CONNECTION_STRING
                , {useNewUrlParser: true, useUnifiedTopology: true})
        .then(() => {
            console.log("Conectado com sucesso")
        }).catch((err) => {
            console.log(err.message)
        })

app.use('/api', arquivoRoute)


app.listen(port, () => {
    console.log(`Servidor ligado em http://localhost:${port}`)
})