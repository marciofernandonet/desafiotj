import { RefObject, SyntheticEvent, useEffect, useMemo, useState } from 'react';
import Box from '@mui/material/Box';
import {
  DataGrid,
  GRID_CHECKBOX_SELECTION_COL_DEF,
  GridColDef
} from '@mui/x-data-grid';
import { GridApiCommunity } from '@mui/x-data-grid/internals';
import DashboardMenu from 'components/common/DashboardMenu';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import { IconifyIcon } from 'components/base/IconifyIcon';
import api from "../../../service/api";
import AccountDialog from '../account/common/AccountDialog';
import { InputLabel, MenuItem, Select, SnackbarCloseReason, TextField } from '@mui/material';
import ProSnackbar from 'layouts/main-layout/common/ProSnackbar';

interface BooksTableProps {
  apiRef: RefObject<GridApiCommunity | null>;
  filterButtonEl: HTMLButtonElement | null;
};

type Book = {
  id: number;
  titulo: string;
  editora: string;
  edicao: number;
  anoPublicacao: string;
  livroAutor: {
    autorId: number;
  }[];
  livroAssunto: {
    assuntoId: number;
  }[];
};

type Author = {
  id: number;
  nome: string;
};

type Subject = {
  id: number;
  descricao: string;
};

const BooksTable = ({ apiRef }: BooksTableProps) => {
  const [books, setBooks] = useState<Book[]>([]);
  const [authors, setAuthors] = useState<Author[]>([]);
  const [subjects, setSubjects] = useState<Subject[]>([]);
  const [id, setId] = useState(0);
  const [title, setTitle] = useState("");
  const [publisher, setPublisher] = useState("");
  const [edition, setEdition] = useState(0);
  const [yearPublic, setYearPublic] = useState("");
  const [author, setAuthor] = useState("");
  const [subject, setSubject] = useState("");
  const [submitted, setSubmitted] = useState(false);
  const [index, setIndex] = useState(-1);
  const [open, setOpen] = useState(false);
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [error, setError] = useState(false);
  const [openUpdate, setOpenUpdate] = useState(false);
  const [openDelete, setOpenDelete] = useState(false);

  useEffect(() => {
    (async () => {
      const [book, author, subject] = await Promise.all([
        api.get("book"),
        api.get("author"),
        api.get("subject")
      ]);
      setBooks(book.data.data);
      setAuthors(author.data.data);
      setSubjects(subject.data.data)
    })();
  }, []);

  const handleOpenUpdate = () => setOpenUpdate(!openUpdate);
  const handleOpenDelete = () => setOpenDelete(!openDelete);

  function handleOpen() {
    setSubmitted(false);
    setTitle("");
    setPublisher("");
    setEdition(0);
    setYearPublic("");
    setAuthor("");
    setSubject("");
    setOpen(!open);
  }

  async function handleModalUpdate(index: number, id: number) {
    try{
      setIndex(index);
      setId(id);
      setOpenUpdate(true);
      const response = await api.get(`book/${id}`);
      if(response.status === 200){
        const { titulo, editora, edicao, anoPublicacao, livroAutor, livroAssunto } = response.data.data as Book;  
        setTitle(titulo);
        setPublisher(editora);
        setEdition(edicao);
        setYearPublic(anoPublicacao);
        setAuthor(String(livroAutor[0].autorId));
        setSubject(String(livroAssunto[0].assuntoId));
      }
    }
    catch(error) {
      console.log(error)
    }
  }

  function handleModalDelete(index: number, id: number) {
    setIndex(index);
    setId(id);
    setOpenDelete(true);
  }

  async function saveBook() {
    const check = !title || !publisher || !edition || !yearPublic || !author || !subject;
    try {
      setSubmitted(true);

      if (check) {
        return;
      }

      const response = await api.post("book", {
        titulo: title,
        editora: publisher,
        edicao: edition,
        anoPublicacao: yearPublic,
        autoresIds: [author],
        assuntosIds: [subject]
      });
      const newBook = response.data.data;
      setBooks(prev => [...prev, newBook]);
      setError(false);
      setOpen(false);
    }
    catch(error) {
      setError(true);
      console.log(error);
    }
    finally {
      if(!check) setOpenSnackbar(true);
    }
  }

  async function updateBook() {
    const check = !title || !publisher || !edition || !yearPublic;
    try {
      setSubmitted(true);

      if (check) {
        return;
      }
      
      const response = await api.put("book", {
        id,
        titulo: title,
        editora: publisher,
        edicao: edition,
        anoPublicacao: yearPublic,
        autorId: author,
        assuntoId: subject
      });
      const updateBook = response.data.data;
      setBooks(prev => [
        ...prev.slice(0, index),
        updateBook,
        ...prev.slice(index + 1),
      ]);
      setOpenUpdate(false);
    }
    catch(error) {
      setError(true);
      console.log(error);
    }
    finally {
      if(!check) setOpenSnackbar(true);
    }
  }

  async function deleteBook() {
    try {  
      const response = await api.delete(`book/${id}`);
      if(response.status === 200){
        setBooks(prev => [
          ...prev.slice(0, index),
          ...prev.slice(index + 1),
        ]);
      }
    }
    catch(error) {
      console.log(error);
    }
    finally {
      setOpenDelete(false);
    }
  }

  const handleClose = (_event: SyntheticEvent, reason?: SnackbarCloseReason) => {
    if (reason === 'clickaway') return;
    setOpenSnackbar(false);
  };

  const columns: GridColDef<Book>[] = useMemo(
    () => [
      {
        ...GRID_CHECKBOX_SELECTION_COL_DEF,
        width: 64,
      },
      {
        field: 'titulo',
        headerName: 'Título',
        minWidth: 160,
        sortable: false,
        filterable: false,
        flex: 1,
      },
      {
        field: 'editora',
        headerName: 'Editora',
        width: 150,
        sortable: false,
        filterable: false,
      },
      {
        field: 'edicao',
        headerName: 'Edição',
        width: 160,
        sortable: false,
        filterable: false,
      },
      {
        field: 'anoPublicacao',
        headerName: 'Ano Publicação',
        width: 160,
        sortable: false,
        filterable: false,
      },
      {
        field: 'action',
        headerName: '',
        filterable: false,
        sortable: false,
        width: 60,
        align: 'right',
        headerAlign: 'right',
        renderCell: (params) => {
          const index = params.api.getRowIndexRelativeToVisibleRows(params.id);
          return (
            <DashboardMenu
              menuItems={[
                {
                  label: 'Editar',
                  onClick: () => handleModalUpdate(index, params.row.id)
                },
                {
                  label: 'Remover',
                  sx: { color: 'error.main' },
                  onClick: () => handleModalDelete(index, params.row.id)
                },
              ]}
            />
          )
        },
      },
    ],
    [],
  );

  return (
    <Box sx={{ width: 1 }}>
      <Stack
        sx={{
          columnGap: 1,
          rowGap: 2,
          justifyContent: 'space-between',
          alignItems: { xl: 'center' },
          flexWrap: { xs: 'wrap', md: 'nowrap' },
          mb: 4
        }}
      >
        <Button
          variant="contained"
          color="primary"
          startIcon={<IconifyIcon icon="material-symbols:add-rounded" />}
          sx={{ flexShrink: 0 }}
          onClick={handleOpen}
        >
          Livro
        </Button>
      </Stack>
      <DataGrid
        rowHeight={64}
        rows={books}
        apiRef={apiRef}
        columns={columns}
        hideFooter
      />

      <AccountDialog
        title="Novo livro"
        open={open}
        handleConfirm={saveBook}
        handleDialogClose={handleOpen}
        handleDiscard={handleOpen}
        sx={{ minWidth: 400, maxWidth: 600 }}
      >
        <Stack direction="column" spacing={1} p={0.125}>
          <TextField
            placeholder="Título"
            label="Título"
            required
            error={submitted && !title}
            helperText={submitted && !title ? "Campo obrigatório" : ""}
            onChange={e => setTitle(e.target.value)}
            slotProps={{
              htmlInput: {
                maxLength: 40,
              },
            }}
            fullWidth
          />
          <TextField
            placeholder="Editora"
            label="Editora"
            required
            error={submitted && !publisher}
            helperText={submitted && !publisher ? "Campo obrigatório" : ""}
            onChange={e => setPublisher(e.target.value)}
            slotProps={{
              htmlInput: {
                maxLength: 40,
              },
            }}
            fullWidth
          />
          <TextField
            placeholder="Edição"
            label="Edição"
            type="number"
            required
            error={submitted && !edition}
            helperText={submitted && !edition ? "Campo obrigatório" : ""}
            onChange={e => setEdition(Number(e.target.value))}
            fullWidth
          />
          <TextField
            placeholder="Ano publicação"
            label="Ano publicação"
            required
            error={submitted && !yearPublic}
            helperText={submitted && !yearPublic ? "Campo obrigatório" : ""}
            onChange={e => setYearPublic(e.target.value)}
            slotProps={{
              htmlInput: {
                maxLength: 4,
              },
            }}
            fullWidth
          />
          <Box>
            <InputLabel>Autor</InputLabel>
            <Select
              required
              error={submitted && !author}
              onChange={e => setAuthor(String(e.target.value))}
              displayEmpty
              fullWidth
            >
              <MenuItem disabled>
                Selecione
              </MenuItem>
              {authors.map(x => (
                <MenuItem value={x.id}>{x.nome}</MenuItem>
              ))}
            </Select>
          </Box>
          <Box>
            <InputLabel>Assunto</InputLabel>
            <Select
              required
              error={submitted && !subject}
              onChange={e => setSubject(String(e.target.value))}
              displayEmpty
              fullWidth
            >
              <MenuItem disabled>
                Selecione
              </MenuItem>
              {subjects.map(x => (
                <MenuItem value={x.id}>{x.descricao}</MenuItem>
              ))}
            </Select>
          </Box>
        </Stack>
      </AccountDialog>
      
      <AccountDialog
        title="Editar livro"
        open={openUpdate}
        handleConfirm={updateBook}
        handleDialogClose={handleOpenUpdate}
        handleDiscard={handleOpenUpdate}
        sx={{ minWidth: 400, maxWidth: 600 }}
      >
        <Stack direction="column" spacing={1} p={0.125}>
          <TextField
            placeholder="Título"
            label="Título"
            required
            error={submitted && !title}
            helperText={submitted && !title ? "Campo obrigatório" : ""}
            value={title}
            onChange={e => setTitle(e.target.value)}
            slotProps={{
              htmlInput: {
                maxLength: 40,
              },
            }}
            fullWidth
          />
          <TextField
            placeholder="Editora"
            label="Editora"
            required
            error={submitted && !publisher}
            helperText={submitted && !publisher ? "Campo obrigatório" : ""}
            value={publisher}
            onChange={e => setPublisher(e.target.value)}
            slotProps={{
              htmlInput: {
                maxLength: 40,
              },
            }}
            fullWidth
          />
          <TextField
            placeholder="Edição"
            label="Edição"
            required
            error={submitted && !edition}
            helperText={submitted && !edition ? "Campo obrigatório" : ""}
            value={edition}
            type="number"
            onChange={e => setEdition(Number(e.target.value))}
            fullWidth
          />
          <TextField
            placeholder="Ano publicação"
            label="Ano publicação"
            required
            error={submitted && !yearPublic}
            helperText={submitted && !yearPublic ? "Campo obrigatório" : ""}
            value={yearPublic}
            onChange={e => setYearPublic(e.target.value)}
            slotProps={{
              htmlInput: {
                maxLength: 4,
              },
            }}
            fullWidth
          />
          <Box>
            <InputLabel>Autor</InputLabel>
            <Select
              required
              error={submitted && !author}
              onChange={e => setAuthor(String(e.target.value))}
              value={author}
              displayEmpty
              fullWidth
            >
              <MenuItem disabled>
                Selecione
              </MenuItem>
              {authors.map(x => (
                <MenuItem value={x.id}>{x.nome}</MenuItem>
              ))}
            </Select>
          </Box>
          <Box>
            <InputLabel>Assunto</InputLabel>
            <Select
              required
              error={submitted && !subject}
              onChange={e => setSubject(String(e.target.value))}
              value={subject}
              displayEmpty
              fullWidth
            >
              <MenuItem disabled>
                Selecione
              </MenuItem>
              {subjects.map(x => (
                <MenuItem value={x.id}>{x.descricao}</MenuItem>
              ))}
            </Select>
          </Box>
        </Stack>
      </AccountDialog>
      
      <AccountDialog
        title="Remover livro"
        subtitle="Deseja confirmar a remoção do livro?"
        open={openDelete}
        handleConfirm={deleteBook}
        handleDialogClose={handleOpenDelete}
        handleDiscard={handleOpenDelete}
        typeDelete
        sx={{ minWidth: 400, maxWidth: 600 }}
      />
      <ProSnackbar open={openSnackbar} onClose={handleClose} error={error} />
    </Box>
  );
};

export default BooksTable;
