const express = require('express')
const bodyParser = require('body-parser')
const cors = require('cors')
const mongoose = require('mongoose')
const Arquivos = require('./data/dataAccess.js')
var port = process.env.PORT || 5000

if(process.env.NODE_ENV !== 'production'){
    const dotenv = require('dotenv')
    dotenv.config()
}

const app = express()
app.use(cors())
app.use(bodyParser.json())
app.use(express.static(__dirname + '/public/'))

mongoose.connect(process.env.CONNECTION_STRING
                , {useNewUrlParser: true, useUnifiedTopology: true})
        .then(() => {
            console.log("Conectado com sucesso")
        }).catch((err) => {
            console.log(err.message)
        })

app.get('/api', (req, res) => {
    Arquivos.find({}).sort('nome').exec((err, arquivos) => {
        res.send(arquivos)
    })
})

app.delete('/api/:id', (req, res) => {
    Arquivos.findById(req.params.id, (err, arquivo) => {
        arquivo.remove((arquivoErr, removedArquivo) => {
            res.send(removedArquivo)
        })
    })
})

app.listen(port, () => {
    console.log(`Servidor ligado em http://localhost:${port}`)
})