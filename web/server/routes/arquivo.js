const router = require('express').Router();
const Arquivo = require('../Models/Arquivo');

router.get('/', async (req, res) => {
    await Arquivo.find({}).sort('nome').exec((err, arquivos) => {
        res.send(arquivos)
    })
})

router.post('/', async (req, res) => {
    const arquivo = new Arquivo({
        nome: req.body.nome,
        conteudo: req.body.conteudo
    })
    try {
        const arquivoSalvo = await arquivo.save()
        res.send(arquivoSalvo)
    } catch (err) {
        res.status(400).send(err)
    }
})

router.delete('/:id', async (req, res) => {
    await Arquivo.findById(req.params.id, (err, arquivo) => {
        arquivo.remove((arquivoErr, removedArquivo) => {
            res.send(removedArquivo)
        })
    })
})

module.exports = router;